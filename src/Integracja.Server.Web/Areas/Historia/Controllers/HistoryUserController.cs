using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Web.Areas.Historia.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.History;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Historia.Controllers
{

    [Area("Historia")]
    public class HistoryUserController : ApplicationController
    {
        public HistoryUserController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int gameId, int userId)
        {
            HistoryUserViewModel Model = new HistoryUserViewModel();
            var user = await UserManager.FindByIdAsync(userId.ToString());
            Model.Username = user.UserName;

            HistoryQuestionModel historyQuestions = await GameService.Get<HistoryQuestionModel>(gameId, UserId);
            HistoryUserModel historyUser = await GameUserService.Get<HistoryUserModel>(gameId, userId);
            
            List<KeyValuePair<int, int?>> questionScore;
            questionScore = FillScore(historyUser);

            List<HistoryUserInfo> HistoryGameUserInfo = new List<HistoryUserInfo>();

            int? points = 0;
            foreach (var k in questionScore)
                if(k.Value != null)
                    points += k.Value;
            

            for(int i = 0; i < historyQuestions.QuestionPool.Count; i++)
            {
                List<string> answers = new List<string>();

                List<int> userAnswers = new List<int>();
                List<int> correctAnswers = new List<int>();
                List<int> correctAnswersMINUSusercorrectAnswers = new List<int>();
                List<int> incorrectAnswers = new List<int>();
                List<int> usercorrectAnswers = new List<int>();
                List<int> userincorrectAnswers = new List<int>();
                List<int> status = new List<int>();

                int index = 0;
                foreach (var element in historyUser.GameQuestions)
                    if (historyQuestions.QuestionPool[i].Id == element.QuestionId)
                        index = element.Index;

                for (int j = 0; j < historyQuestions.QuestionPool[i].Answers.Count; j++)
                {
                    answers.Add(historyQuestions.QuestionPool[i].Answers[j].Content);
                    if (historyQuestions.QuestionPool[i].Answers[j].IsCorrect)
                        correctAnswers.Add(j);
                    else
                        incorrectAnswers.Add(j);
                    foreach (var k in historyUser.UserAnswerPool)
                    {
                        if(historyQuestions.QuestionPool[i].Id == k.UserQuestionId)
                            if(historyQuestions.QuestionPool[i].Answers[j].Id == k.UserAnswerId)
                                userAnswers.Add(j);
                    }
                }

                foreach(var k in userAnswers)
                    foreach(var l in correctAnswers)
                        if (k == l)
                            usercorrectAnswers.Add(k);

                if (usercorrectAnswers.Count == 0)
                    correctAnswersMINUSusercorrectAnswers = correctAnswers;
                else
                {
                    foreach (var k in correctAnswers)
                        foreach (var l in usercorrectAnswers)
                        {
                            if (k == l)
                                break;
                            if (l == usercorrectAnswers[usercorrectAnswers.Count - 1])
                                correctAnswersMINUSusercorrectAnswers.Add(k);
                        }
                }

                if (usercorrectAnswers.Count == 0)
                    userincorrectAnswers = userAnswers;
                else
                {
                    foreach (var u in userAnswers)
                        foreach (var l in usercorrectAnswers)
                        {
                            if (u == l)
                                break;
                            if (l == usercorrectAnswers[usercorrectAnswers.Count - 1])
                                userincorrectAnswers.Add(u);
                        }
                }

                for (int j = 0; j < historyQuestions.QuestionPool[i].Answers.Count; j++)
                {
                    foreach(var k in usercorrectAnswers)
                    {
                        if (j == k)
                            status.Add(0);
                    }
                    if(status.Count < j + 1)
                    {
                        foreach (var k in correctAnswersMINUSusercorrectAnswers)
                        {
                            if (j == k)
                                status.Add(1);
                        }
                    }
                    if (status.Count < j + 1)
                    {
                        foreach (var k in userincorrectAnswers)
                        {
                            if (j == k)
                                status.Add(2);
                        }
                    }
                    if (status.Count < j + 1)
                    {
                        status.Add(3);
                    }
                }

                if(usercorrectAnswers.Count == 0 && userincorrectAnswers.Count == 0)
                    status.Add(4);

                int? pointsReceived = 0;
                foreach (var k in questionScore)
                {
                    if (k.Key == historyQuestions.QuestionPool[i].Id)
                        pointsReceived = k.Value;
                }

                HistoryUserInfo UserInfo = new HistoryUserInfo
                {
                    index = index,
                    questionContent = historyQuestions.QuestionPool[i].Content,
                    answers = answers,
                    status = status,
                    pointsReceived = pointsReceived,
                    positivePoints = historyQuestions.QuestionPool[i].PositivePoints,
                    negativePoints = historyQuestions.QuestionPool[i].NegativePoints

                };

                HistoryGameUserInfo.Add(UserInfo);
            }

            Model.Points = points;
            Model.HistoryGameUserInfo = HistoryGameUserInfo.OrderBy(o => o.index).ToList();

            return View("HistoryUser", Model);
        }

        List<KeyValuePair<int, int?> > FillScore(HistoryUserModel historyUser)
        {
            List<KeyValuePair<int, int?>> questionScore = new List<KeyValuePair<int, int?>>();

            foreach(var k in historyUser.GameUserQuestions)
            {
                questionScore.Add(new KeyValuePair<int, int?>(k.QuestionId, (int?)k.QuestionScore));
            }

            return questionScore;
        }

    }
}
