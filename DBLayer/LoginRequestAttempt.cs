using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class LoginRequestAttempt
    {
        public long Rid { get; set; }
        public string ClientId { get; set; }
        public string KeyToAuthenticate { get; set; }
        public string AttemptUser { get; set; }
        public int? IsActive { get; set; }
        public DateTime UseTime { get; internal set; }
        public DateTime CreateTime { get; internal set; }
    }
}
