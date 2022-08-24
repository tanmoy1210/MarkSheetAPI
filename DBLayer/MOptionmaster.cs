using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MOptionmaster
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDesc { get; set; }
        public string MToolTip { get; set; }
        public string MenuGrpId { get; set; }
        public string MenuType { get; set; }
        public string PageLink { get; set; }
        public string PageLevel { get; set; }
        public string Settings { get; set; }
    }
}
