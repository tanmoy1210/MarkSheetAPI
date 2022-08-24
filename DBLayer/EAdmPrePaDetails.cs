using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class EAdmPrePaDetails
    {
        public long RecordId { get; set; }
        public long? Aid { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLocation { get; set; }
        public int? ClassId { get; set; }
        public int? AyId { get; set; }
        public float? Percentage { get; set; }
        public string Grade { get; set; }
    }
}
