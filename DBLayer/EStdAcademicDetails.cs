using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class EStdAcademicDetails
    {
        public long StdAdId { get; set; }
        public string AyId { get; set; }
        public string ClassId { get; set; }
        public int? RollNo { get; set; }
        public int? Status { get; set; }
        public float? Percentage { get; set; }
        public string Grade { get; set; }
    }
}
