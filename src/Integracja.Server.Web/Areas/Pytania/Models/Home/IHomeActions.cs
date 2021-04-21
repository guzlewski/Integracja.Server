using System.Threading.Tasks;
using Integracja.Server.Web.Areas.Pytania.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Integracja.Server.Web.Areas.Pytania.Models.Home
{
    public interface IHomeActions : IHomeNav, IQuestionTableActions
    {
    }
}
