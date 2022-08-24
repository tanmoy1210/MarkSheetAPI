using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT
{
    public interface IJwtAuthenticationManager
    {
        //string Authenticate(string username, string pasword);
        JObject Authenticate(JObject data, IConfiguration configuration);
    }
}
