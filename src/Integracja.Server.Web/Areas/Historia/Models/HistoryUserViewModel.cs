using System.Collections.Generic;

namespace Integracja.Server.Web.Areas.Historia.Models
{
    public class HistoryUserViewModel
    {
        public string Username { get; set; }
        public int? Points { get; set; }
        public List<HistoryUserInfo> HistoryGameUserInfo { get; set; }
    }

    public class HistoryUserInfo
    {
        public int index;
        public string questionContent;
        public List<string> answers;
        public List<int> status;
        public int? pointsReceived;
        public int positivePoints;
        public int negativePoints;
    }
}
