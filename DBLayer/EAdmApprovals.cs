using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class EAdmApprovals
    {
        public long ApvId { get; set; }
        public long? Aid { get; set; }
        public string Remarks { get; set; }
        public long? ApprovedBy { get; set; }
        public int? Status { get; set; }
    }
}
