using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MState
    {
        public int StateId { get; set; }
        public int? CountryId { get; set; }
        public string StateName { get; set; }

        public virtual MCountries Country { get; set; }
    }
}
