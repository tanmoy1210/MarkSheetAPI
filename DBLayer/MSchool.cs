using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MSchool
    {
        public long SchoolId { get; set; }
        public long? ClientId { get; set; }
        public int? MenuId { get; set; }
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public int? BankId { get; set; }
        public string AccountHolderName { get; set; }
        public int? StateId { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? Status { get; set; }

        public virtual MClient Client { get; set; }
    }
}
