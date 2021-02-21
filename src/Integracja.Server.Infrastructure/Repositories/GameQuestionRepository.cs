using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GameQuestionRepository : IGameQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameQuestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameQuestion> GetQuestion(int gameId, int userId)
        {
            var x = await _dbContext.GameUsers
                .AsNoTracking()
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId && gu.State == GameUserState.Active &&
                    gu.Game.GameState == GameState.Normal &&
                    gu.Game.EndTime > DateTimeOffset.Now)
                .Select(gu => new
                {
                    GameUser = gu,
                    QuestionsCount = gu.Game.Questions.Count
                })
                .FirstOrDefaultAsync();

            var gameUser = x.GameUser;

            if (gameUser == null)
            {
                throw new NotFoundException();
            }

            int answeredQuestionsCount = gameUser.AnsweredQuestions;

            if (answeredQuestionsCount == x.QuestionsCount)
            {
                throw new BadRequestException("You already finished this game.");
            }

            if (answeredQuestionsCount == 0)
            {
                gameUser.GameStartTime = DateTimeOffset.Now;
            }

            var entity = await _dbContext.GameQuestions
                .AsNoTracking()
                .Where(gq => gq.GameId == gameId &&
                    gq.Index == gameUser.AnsweredQuestions)
                .Include(gq => gq.Question)
                .ThenInclude(q => q.Answers)
                .Select(gq => new
                {
                    GameQuestion = gq,
                    GameUserQuestion = gq.GameUserQuestions
                        .Where(guq => guq.UserId == userId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (entity.GameUserQuestion == null)
            {
                var gameUserQuestion = new GameUserQuestion
                {
                    GameId = gameId,
                    UserId = userId,
                    QuestionId = entity.GameQuestion.QuestionId,
                    QuestionDownloadTime = DateTimeOffset.Now
                };

                await _dbContext.AddAsync(gameUserQuestion);
                await _dbContext.SaveChangesAsync();
            }

            return entity.GameQuestion;
        }

        public async Task<GameUserQuestion> SaveAnswers(int gameId, int userId, ICollection<int> answers)
        {
            var x = await _dbContext.GameUsers
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId && gu.State == GameUserState.Active &&
                    gu.Game.GameState == GameState.Normal &&
                    gu.Game.EndTime > DateTimeOffset.Now)
                .Select(gu => new { GameUser = gu, QuestionsCount = gu.Game.Questions.Count })
                .FirstOrDefaultAsync();

            if (x == null)
            {
                throw new NotFoundException();
            }

            var gameUser = x.GameUser;
            int answeredQuestionsCount = gameUser.AnsweredQuestions;

            var entity = await _dbContext.GameQuestions
                .Where(gq => gq.GameId == gameId &&
                    gq.Index == gameUser.AnsweredQuestions)
                .Include(gq => gq.Question)
                .ThenInclude(q => q.Answers)
                .Select(gq => new
                {
                    GameQuestion = gq,
                    GameUserQuestion = gq.GameUserQuestions
                        .Where(guq => guq.UserId == userId)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ConflictException($"You already answered this question.");
            }

            if(entity.GameUserQuestion == null)
            {
                throw new ConflictException($"You have to download question first.");
            }

            var validatedAnswers = entity.GameQuestion.Question.Answers
               .Intersect(answers.Select(answerId => new Answer
               {
                   Id = answerId
               }), new AnswerEqualityComparer());

            if (validatedAnswers.Count() != answers.Count)
            {
                throw new BadRequestException();
            }

            foreach (var answer in validatedAnswers)
            {
                _dbContext.Add(new GameUserQuestionAnswer
                {
                    GameId = gameId,
                    UserId = userId,
                    QuestionId = entity.GameQuestion.QuestionId,
                    AnswerId = answer.Id
                });
            }

            switch (entity.GameQuestion.Question.QuestionScoring)
            {
                case QuestionScoring.ScorePerGoodAnswer:

                    entity.GameUserQuestion.QuestionScore = 0;

                    foreach (var answer in validatedAnswers)
                    {
                        if (answer.IsCorrect)
                        {
                            entity.GameUserQuestion.QuestionScore += Or(entity.GameQuestion.OverridePositivePoints,
                                entity.GameQuestion.Question.PositivePoints);
                        }
                        else
                        {
                            entity.GameUserQuestion.QuestionScore += Or(entity.GameQuestion.OverrideNegativePoints,
                                entity.GameQuestion.Question.NegativePoints);
                        }
                    }

                    if (entity.GameUserQuestion.QuestionScore >= 0)
                    {
                        gameUser.CorrectlyAnsweredQuestions++;
                    }
                    else
                    {
                        gameUser.IncorrectlyAnsweredQuestions++;
                    }

                    break;
                case QuestionScoring.ScoreIfFullyCorrect:

                    var correct = 0;
                    bool incorrect = false;

                    foreach (var answer in validatedAnswers)
                    {
                        if (answer.IsCorrect)
                        {
                            correct++;
                        }
                        else
                        {
                            incorrect = true;
                        }
                    }

                    if (!incorrect && correct == entity.GameQuestion.Question.Answers.Where(q => q.IsCorrect).Count())
                    {
                        entity.GameUserQuestion.QuestionScore = Or(entity.GameQuestion.OverridePositivePoints,
                               entity.GameQuestion.Question.PositivePoints);
                        gameUser.CorrectlyAnsweredQuestions++;
                    }
                    else
                    {
                        entity.GameUserQuestion.QuestionScore = Or(entity.GameQuestion.OverrideNegativePoints,
                               entity.GameQuestion.Question.NegativePoints);
                        gameUser.IncorrectlyAnsweredQuestions++;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            entity.GameUserQuestion.IsAnswered = true;
            entity.GameUserQuestion.QuestionAnswerTime = DateTimeOffset.Now;

            gameUser.AnsweredQuestions++;

            if (gameUser.AnsweredQuestions == x.QuestionsCount)
            {
                gameUser.GameEndTime = DateTimeOffset.Now;

                var sum = _dbContext.GameUserQuestions.Where(guq => guq.GameId == gameId &&
                    guq.UserId == userId)
                .Sum(guq => guq.QuestionScore);

                sum += entity.GameUserQuestion.QuestionScore;

                gameUser.GameScore = sum;
            }

            await _dbContext.SaveChangesAsync();

            return entity.GameUserQuestion;
        }

        private static float Or(float? x, float y)
        {
            if (x.HasValue)
            {
                return x.Value;
            }

            return y;
        }
    }
}
