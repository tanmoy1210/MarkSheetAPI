using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MT.DBLayer;
 using MT.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MT
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string> { { "test1", "password" }, { "test2", "password" } };
        private readonly string key;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public JObject Authenticate(JObject data, IConfiguration configuration)
        {
           Logins objUser_AccessCredential = new Logins();
            try
            {

                Common.ValidDatePostedData(data, "data");
                using (mararkContext dcdc = AppSettingValue.GetConnectionObject(configuration))
                {

                    List<LoginRequestAttempt> objul = new List<LoginRequestAttempt>();
                    objul = (from s in dcdc.LoginRequestAttempt where s.ClientId == data["SHA"]["clientid"].ToString() && s.KeyToAuthenticate == data["SHA"]["token"].ToString() && s.IsActive == 1 select s).ToList();

                    if (objul.Count > 0)
                    {
                        objUser_AccessCredential = dcdc.Logins.SingleOrDefault(x => (x.UserId == data["data"]["UserID"].ToString() && x.IsActive == 1));
                        if (objUser_AccessCredential == null)
                        {
                            throw new Exception("Username or Password wrong, please try again! ");
                        }

                        else
                        {
                            //HAVE TO MAKE IMPLIMEN FOR SA256 ON BOTH SIDE C# & JAVASRIPT

                            string s = Common.GenerateSHA256String(Common.GenerateSHA256String(data["SHA"]["token"].ToString()) + "" + objUser_AccessCredential.Password);
                            if (data["data"]["Password"].ToString() == s)
                            {
                                foreach (LoginRequestAttempt obju in objul)
                                {
                                    obju.IsActive = 0;
                                    obju.AttemptUser = data["data"]["UserID"].ToString();
                                    obju.UseTime = System.DateTime.Now;
                                }


                                dcdc.SaveChanges();
                            }
                            else
                                throw new Exception("Username or Password wrong, please try again! ");
                        }
                    }
                    else
                        throw new Exception("Unauthorized Access");

                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, data["data"]["UserID"].ToString()),
                     new Claim(ClaimTypes.Role, objUser_AccessCredential.Utype.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                //  return tokenHandler.WriteToken(token);

                Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
                    {
                        { "UserId", objUser_AccessCredential.UserId },
                        { "Name", objUser_AccessCredential.Name },
                        { "PImage", objUser_AccessCredential.ProImage },
                        {"UType",objUser_AccessCredential.Utype },
                        {"token",tokenHandler.WriteToken(token) }

                    };
                return o;

            }
            catch
            {
                throw;
            }
        }




        //public string Authenticate(string username, string password)
        //{
        //    if (users.Any(u => u.Key == username && u.Value == password))
        //    {

        //    }
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenKey = Encoding.ASCII.GetBytes(key);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, username)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(24),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
