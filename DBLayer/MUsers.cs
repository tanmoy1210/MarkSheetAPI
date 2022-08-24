using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MUsers
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string UserPassword { get; set; }
        public long? RoleId { get; set; }
        public long? UserRegId { get; set; }
        public int? Status { get; set; }
        public string? ClientId { get; set; }
        public DateTime? LastStatusAt { get; set; }
        //public virtual MClient Client { get; set; }
        //public virtual MUserTypes Role { get; set; }
    }
}
