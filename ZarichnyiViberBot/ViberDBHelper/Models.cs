using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZarichnyiViberBot.ViberDBHelper
{
    public class WalkAnalytics
    {
            public int CountOfWalk { get; set; }
            public decimal TotalDistance { get; set; }
            public TimeSpan TotalTime { get; set; }
    }

    public class TopWalk
    {
        public int WalkNumber { get; set; }
        public decimal Distance { get; set; }
        public TimeSpan TimeWalk { get; set; }
    }
}
