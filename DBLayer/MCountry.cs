using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MCountry
    {
        public long Cid { get; set; }
        public string Cname { get; set; }
        public int? Isactive { get; set; }
    }
}
