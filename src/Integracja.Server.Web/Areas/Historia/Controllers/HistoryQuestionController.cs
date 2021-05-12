using AutoMapper;
using Integracja.Server.Core.Models.Identity;
using Integracja.Server.Infrastructure.Data;
using Integracja.Server.Infrastructure.Services.Implementations;
using Integracja.Server.Web.Areas.Historia.Models;
using Integracja.Server.Web.Controllers;
using Integracja.Server.Web.Models.Shared.History;
using Integracja.Server.Web.Models.Shared.Question;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integracja.Server.Web.Areas.Historia.Controllers
{
    [Area("Historia")]
    public class HistoryQuestionController : ApplicationController
    {
        public HistoryQuestionController(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper) : base(userManager, dbContext, mapper)
        {
        }

        public async Task<IActionResult> Index(int gameId, int questionId)
        {
            HistoryQuestionViewModel Model = new HistoryQuestionViewModel();
            Model.GameId = gameId;

            Model.question = await QuestionService.Get<QuestionModel>(questionId, UserId);
            HistoryUserModel users = await GameService.Get<HistoryUserModel>(gameId, UserId);

            List<guser> usersStats = new List<guser>();
            string username;
            int? questionScore;

            foreach (var element in users.GameUsers)
            {
                int counter = 0;
                foreach (var element2 in users.GameUserQuestions)
                {
                    if (element.UserId == element2.UserId && questionId == element2.QuestionId)
                        break;
                    else if (counter == users.GameUserQuestions.Count - 1)
                    {
                        var user = await UserManager.FindByIdAsync(element.UserId.ToString());
                        username = user.UserName;

                        List<int> answersState = new List<int>();
                        answersState.Add(-2);

                        guser userStats = new guser
                        {
                            username = username,
                            questionScore = 0,
                            answersState = answersState
                        };

                        usersStats.Add(userStats);
                    }
                    counter++;
                }
            }

            foreach (var element in users.GameUserQuestions)
            {
                if (element.QuestionId == Model.question.Id)
                {
                    var user = await UserManager.FindByIdAsync(element.UserId.ToString());
                    username = user.UserName;
                    questionScore = (int?)element.QuestionScore;
                    if (questionScore == null)
                        questionScore = 0;

                    HistoryUserModel historyUser = await GameUserService.Get<HistoryUserModel>(gameId, element.UserId);

                    List<int> userAnswers = new List<int>();
                    foreach (var k in historyUser.UserAnswerPool)
                        if (k.UserQuestionId == questionId)
                            userAnswers.Add(k.UserAnswerId);

                    List<int> answersState = new List<int>();

                    if (element.IsAnswered == false)
                        answersState.Add(-2);

                    for (int i = 0; i < Model.question.Answers.Count; i++)
                    {
                        for (int j = 0; j < userAnswers.Count; j++)
                        {
                            if (Model.question.Answers[i].Id == userAnswers[j] && Model.question.Answers[i].IsCorrect)
                            {
                                answersState.Add(1);
                                break;
                            }
                            else if (Model.question.Answers[i].Id == userAnswers[j] && !Model.question.Answers[i].IsCorrect)
                            {
                                answersState.Add(-1);
                                break;
                            }
                            if (j == userAnswers.Count - 1)
                                answersState.Add(0);
                        }
                    }

                    guser userStats = new guser
                    {
                        username = username,
                        questionScore = questionScore,
                        answersState = answersState
                    };

                    usersStats.Add(userStats);
                }
            }

            usersStats = usersStats.OrderByDescending(o => o.questionScore).ToList();

            Model.usersStats = usersStats;

            return View("HistoryQuestion", Model);
        }
    }
}
