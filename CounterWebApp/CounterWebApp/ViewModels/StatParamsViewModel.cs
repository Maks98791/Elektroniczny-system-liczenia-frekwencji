using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CounterWebApp.ViewModels
{
    public class StatParamsViewModel
    {
        [Display(Name = "Data początkowa")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "Data końcowa")]
        public DateTime EndDate { get; set; }
    }
}
