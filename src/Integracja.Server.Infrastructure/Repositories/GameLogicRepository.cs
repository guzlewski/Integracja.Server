using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Joins;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Enums;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Repositories
{
    public class GameLogicRepository : IGameLogicRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameLogicRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<GameUserQuestion>> GetQuestion(int gameId, int userId)
        {
            var entity = await _dbContext.GameUsers
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId &&
                    gu.GameUserState == GameUserState.Active &&
                    gu.Game.GameState != GameState.Deleted)
                .Select(gu => new
                {
                    GameUser = gu,
                    gu.Game.Gamemode,
                    Game = gu.Game
                })
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            if (entity.Game.GameState != GameState.Normal)
            {
                throw new ConflictException(ErrorCode.GameHasBeenCancelled);
            }

            var now = DateTimeOffset.Now;

            if (entity.Game.EndTime <= now)
            {
                throw new ConflictException(ErrorCode.GameHasEnded);
            }

            if (entity.GameUser.AnsweredQuestions == entity.Game.QuestionsCount)
            {
                throw new ConflictException(ErrorCode.AlreadyAnsweredAllQuestions);
            }

            if (entity.GameUser.GameOver)
            {
                throw new ConflictException(ErrorCode.GameIsOver);
            }

            var entitiesChanged = false;

            if (entity.GameUser.AnsweredQuestions == 0)
            {
                entity.GameUser.GameStartTime = now;
                entitiesChanged = true;
            }

            if (entity.Gamemode.TimeForFullQuiz != null && (now - entity.GameUser.GameStartTime.Value).TotalSeconds > entity.Gamemode.TimeForFullQuiz)
            {
                entity.GameUser.GameOver = true;
                await _dbContext.SaveChangesAsync();

                throw new ConflictException(ErrorCode.GameTimeHasExpired);
            }

            var secondEntities = await _dbContext.GameQuestions
                .Where(gq => gq.GameId == gameId &&
                    gq.Index == entity.GameUser.AnsweredQuestions)
                .Select(gq => new
                {
                    GameUserQuestion = gq.GameUserQuestions
                        .Where(guq => guq.UserId == userId)
                        .FirstOrDefault(),
                    gq.QuestionId
                })
                .FirstOrDefaultAsync();

            if (secondEntities.GameUserQuestion == null)
            {
                await _dbContext.AddAsync(new GameUserQuestion
                {
                    GameId = gameId,
                    UserId = userId,
                    QuestionId = secondEntities.QuestionId,
                    QuestionDownloadTime = now
                });
                entitiesChanged = true;
            }

            if (entitiesChanged)
            {
                await _dbContext.SaveChangesAsync();
            }

            return _dbContext.GameUserQuestions
                .AsNoTracking()
                .Where(gu => gu.GameId == gameId &&
                    gu.UserId == userId &&
                    gu.QuestionId == secondEntities.QuestionId);
        }

        public async Task<IQueryable<GameUserQuestion>> SaveAnswers(int gameId, int userId, int questionId, IEnumerable<int> asnwers)
        {
            var gameUserQuestionEntity = await _dbContext.GameUserQuestions
                .Where(guq => guq.GameId == gameId &&
                    guq.UserId == userId &&
                    guq.QuestionId == questionId &&
                    guq.Game.GameState != GameState.Deleted &&
                    guq.GameUser.GameUserState == GameUserState.Active)
                .Include(guq => guq.Game)
                    .ThenInclude(g => g.Gamemode)
                .Include(guq => guq.Question)
                    .ThenInclude(q => q.Answers)
                .Include(guq => guq.GameUser)
                .Include(guq => guq.GameQuestion)
                .FirstOrDefaultAsync();

            if (gameUserQuestionEntity == null)
            {
                throw new NotFoundException();
            }

            if (gameUserQuestionEntity.Game.GameState != GameState.Normal)
            {
                throw new ConflictException(ErrorCode.GameHasBeenCancelled);
            }

            var now = DateTimeOffset.Now;

            if (gameUserQuestionEntity.Game.EndTime <= now)
            {
                throw new ConflictException(ErrorCode.GameHasEnded);
            }

            if (gameUserQuestionEntity.IsAnswered)
            {
                throw new ConflictException(ErrorCode.AlreadyAnsweredThisQuestion);
            }

            if (gameUserQuestionEntity.GameUser.GameOver)
            {
                throw new ConflictException(ErrorCode.GameIsOver);
            }

            if (gameUserQuestionEntity.Game.Gamemode.TimeForOneQuestion != null && (now - gameUserQuestionEntity.QuestionDownloadTime.Value).TotalSeconds > gameUserQuestionEntity.Game.Gamemode.TimeForOneQuestion)
            {
                gameUserQuestionEntity.GameUser.GameOver = true;
                await _dbContext.SaveChangesAsync();

                throw new ConflictException(ErrorCode.QuestionTimeHasExpired);
            }

            if (gameUserQuestionEntity.Game.Gamemode.TimeForFullQuiz != null && (now - gameUserQuestionEntity.GameUser.GameStartTime.Value).TotalSeconds > gameUserQuestionEntity.Game.Gamemode.TimeForFullQuiz)
            {
                gameUserQuestionEntity.GameUser.GameOver = true;
                await _dbContext.SaveChangesAsync();

                throw new ConflictException(ErrorCode.GameTimeHasExpired);
            }

            var correctAnswers = gameUserQuestionEntity.Question.Answers
                .Where(a => a.IsCorrect)
                .Select(a => a.Id);

            var selectedAnswers = asnwers
                .Where(id => gameUserQuestionEntity.Question.Answers.Any(a => a.Id == id));

            foreach (var answer in selectedAnswers)
            {
                _dbContext.Add(new GameUserQuestionAnswer
                {
                    GameId = gameId,
                    UserId = userId,
                    QuestionId = questionId,
                    AnswerId = answer
                });
            }

            var questionScore = ValidateAnswers(correctAnswers,
                selectedAnswers,
                gameUserQuestionEntity.GameQuestion.OverridePositivePoints ?? gameUserQuestionEntity.Question.PositivePoints,
                gameUserQuestionEntity.GameQuestion.OverrideNegativePoints ?? gameUserQuestionEntity.Question.NegativePoints,
                gameUserQuestionEntity.Question.QuestionScoring);

            gameUserQuestionEntity.IsAnswered = true;
            gameUserQuestionEntity.QuestionAnswerTime = now;
            gameUserQuestionEntity.QuestionScore = questionScore;
            gameUserQuestionEntity.GameUser.AnsweredQuestions++;
            gameUserQuestionEntity.GameUser.GameScore = gameUserQuestionEntity.GameUser.GameScore.GetValueOrDefault() + questionScore;

            if (questionScore > 0)
            {
                gameUserQuestionEntity.GameUser.CorrectlyAnsweredQuestions++;
            }
            else
            {
                gameUserQuestionEntity.GameUser.IncorrectlyAnsweredQuestions++;
            }

            if (gameUserQuestionEntity.Game.Gamemode.NumberOfLives != null && gameUserQuestionEntity.GameUser.IncorrectlyAnsweredQuestions > gameUserQuestionEntity.Game.Gamemode.NumberOfLives)
            {
                gameUserQuestionEntity.GameUser.GameOver = true;
                gameUserQuestionEntity.GameUser.GameEndTime = now;
            }

            if (gameUserQuestionEntity.GameUser.AnsweredQuestions == gameUserQuestionEntity.Game.QuestionsCount)
            {
                gameUserQuestionEntity.GameUser.GameEndTime = now;
            }

            await _dbContext.SaveChangesAsync();

            return _dbContext.GameUserQuestions.Where(guq => guq.GameId == gameId &&
                guq.UserId == userId &&
                guq.QuestionId == questionId);
        }

        private static float ValidateAnswers(IEnumerable<int> correct, IEnumerable<int> selected, float positive, float negative, QuestionScoring scoring)
        {
            var good = selected
                .Where(a => correct.Contains(a))
                .Count();

            var bad = selected.Count() - good;

            return scoring switch
            {
                QuestionScoring.ScorePerGoodAnswer => ScorePerAnswer(good, bad, positive, negative),
                QuestionScoring.ScoreIfFullyCorrect => ScoreForFullQuestion(good, bad, positive, negative, correct.Count()),
                _ => throw new NotImplementedException()
            };
        }

        private static float ScorePerAnswer(int good, int bad, float positive, float negative)
        {
            return good * positive + bad * negative;
        }

        private static float ScoreForFullQuestion(int good, int bad, float positive, float negative, int goodCount)
        {
            if (bad != 0 || good != goodCount)
            {
                return negative;
            }

            return positive;
        }
    }
}
