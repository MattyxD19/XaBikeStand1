using System;
using System.Collections.Generic;
using System.Text;


namespace XaBikeStand.Models
{
    public class MasterMenuItems
    {
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public Type TargetViewModel { get; set; }
    }
}
