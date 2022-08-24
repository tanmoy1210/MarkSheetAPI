using System;
using System.Collections.Generic;

namespace MT.DBLayer
{
    public partial class MMenuRolePermission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public string Permission { get; set; }
    }
}
