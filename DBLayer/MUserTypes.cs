using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MUserTypes
    {
        public MUserTypes()
        {
           // MUsers = new HashSet<MUsers>();
        }

        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public int Status { get; set; }

        //public virtual ICollection<MUsers> MUsers { get; set; }
    }
}
