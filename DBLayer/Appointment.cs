using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class Appointment
    {
        public int Aid { get; set; }
        public int? Cid { get; set; }
        public string MName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Doctorname { get; set; }
        public string File { get; set; }
        public string Medicinename { get; set; }
    }
}
