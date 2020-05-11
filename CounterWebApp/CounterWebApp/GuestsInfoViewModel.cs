using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterWebApp
{
    public class GuestsInfoViewModel
    {
        public int GuestsIn { get; set; }
        public int GuestsOut { get; set; }
        public DateTime CurrentDate { get; set; }
        public int GuestsInside { get; set; }
    }
}
