using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MClient
    {
        public MClient()
        {
            MSchool = new HashSet<MSchool>();
            //MUsers = new HashSet<MUsers>();
        }

        public long ClientId { get; set; }
        public int? MenuId { get; set; }
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public DateTime? RegDate { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<MSchool> MSchool { get; set; }
        //public virtual ICollection<MUsers> MUsers { get; set; }
    }
}
