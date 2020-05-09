using System;
using System.Collections.Generic;

namespace CounterWebApp.Models
{
    public partial class Visitors
    {
        public int RaportId { get; set; }
        public int CameraId { get; set; }
        public int GuestsIn { get; set; }
        public int GuestsOut { get; set; }
        public DateTime RaportDate { get; set; }

        public virtual Cameras Camera { get; set; }
    }
}
