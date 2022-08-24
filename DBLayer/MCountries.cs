using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MCountries
    {
        public MCountries()
        {
            MState = new HashSet<MState>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<MState> MState { get; set; }
    }
}
