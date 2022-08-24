using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MMenuGrpMaster
    {
        public int MenuGrpId { get; set; }
        public string MenuGrpName { get; set; }
        public string MenuGrpDesc { get; set; }
        public string UnderMenuGrpId { get; set; }
    }
}
