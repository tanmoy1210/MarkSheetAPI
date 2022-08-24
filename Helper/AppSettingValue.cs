using MT.DBLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.Helper
{
    public class AppSettingValue
    {
        public static mararkContext GetConnectionObject(IConfiguration _configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<mararkContext>();
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("MT"));
            return new mararkContext(optionsBuilder.Options);
        }
    }
}

//{
//  "Modules": {
//    "Logging": {
//      "logDb": "Server=myServer;Database=myDb1;Trusted_Connection=True;",
//    },
//    "Tenants": {
//      "tenantsDb": "Server=myServer;Database=myDb1;Trusted_Connection=True;",
//    }
//  } 
//}



//string logDbConnectionString = _configuration.GetValue<string>("Modules:Logging:logDb"); // read logDb connection string example
//string tenantsDbConnectionString = _configuration.GetValue<string>("Modules:Tenants:tenantsDb"); // read tenantsDb connection string example