using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatisticsWebService
{

    [Serializable()]
    public class Statistics
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string GameWon { get; set; }
        public string GameLose { get; set; }
        public string Draw { get; set; }
    }

    [Serializable()]
    public class StatisticsList
    {
        public List<Statistics> data { get; set; }
    }
}