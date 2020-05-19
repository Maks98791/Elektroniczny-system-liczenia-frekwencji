using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterWebApp.ViewModels
{
    public class DailyCounterViewModel
    {
        public DateTime Date { get; set; }
        public int Visitors { get; set; }
    }
}
