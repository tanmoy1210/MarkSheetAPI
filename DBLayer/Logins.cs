using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class Logins
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ProImage { get; set; }
        public int? Utype { get; set; }
        public int? IsActive { get; set; }
        public string DeviceId { get; set; }
    }
}
