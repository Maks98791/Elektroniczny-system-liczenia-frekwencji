using System;
using System.Collections.Generic;

namespace CounterWebApp.Models
{
    public partial class Photos
    {
        public int PhotoId { get; set; }
        public int CameraId { get; set; }
        public DateTime RaportDate { get; set; }
        public string PhotoPath { get; set; }

        public virtual Cameras Camera { get; set; }
    }
}
