using System;
using System.Collections.Generic;

namespace CounterWebApp.Models
{
    public partial class Cameras
    {
        public Cameras()
        {
            Photos = new HashSet<Photos>();
            Visitors = new HashSet<Visitors>();
        }

        public int CameraId { get; set; }
        public string CameraLocation { get; set; }

        public virtual ICollection<Photos> Photos { get; set; }
        public virtual ICollection<Visitors> Visitors { get; set; }
    }
}
