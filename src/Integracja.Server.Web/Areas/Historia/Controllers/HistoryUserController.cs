using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Web.Areas.Historia.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.Game;
using Integracja.Server.Web.Models.Shared.History;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            
            List<HistoryUserInfo> HistoryGameUserInfo = new List<HistoryUserInfo>();

            int points = 0;
            for(int i = 0; i < historyQuestions.QuestionPool.Count; i++)
            {
                List<string> answers = new List<string>();
                int correctAnswer = 0, userAnswer = 0;
                for (int j = 0; j < historyQuestions.QuestionPool[i].Answers.Count; j++)
                {
                    answers.Add(historyQuestions.QuestionPool[i].Answers[j].Content);
                    if (historyQuestions.QuestionPool[i].Answers[j].IsCorrect)
                        correctAnswer = j;
                    foreach(var k in historyUser.UserAnswerPool)
                    {
                        if(historyQuestions.QuestionPool[i].Id == k.UserQuestionId)
                            if(historyQuestions.QuestionPool[i].Answers[j].Id == k.UserAnswerId)
                                userAnswer = j;
                    }
                }
                int pointsReceived = historyQuestions.QuestionPool[i].NegativePoints;
                if (correctAnswer == userAnswer)
                    pointsReceived = historyQuestions.QuestionPool[i].PositivePoints;
                points += pointsReceived;

                HistoryUserInfo UserInfo = new HistoryUserInfo
                {
                    questionContent = historyQuestions.QuestionPool[i].Content,
                    answers = answers,
                    correctAnswerId = correctAnswer,
                    userAnswerId = userAnswer,
                    pointsReceived = pointsReceived,
                    positivePoints = historyQuestions.QuestionPool[i].PositivePoints,
                    negativePoints = historyQuestions.QuestionPool[i].NegativePoints
                };

                HistoryGameUserInfo.Add(UserInfo);
            }

            Model.Points = points;
            Model.HistoryGameUserInfo = HistoryGameUserInfo;

            return View("HistoryUser", Model);
        }


    }
}
