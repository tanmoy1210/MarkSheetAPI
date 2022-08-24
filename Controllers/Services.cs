using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using MT.DBLayer;
using MT.Helper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace MT.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class Services : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _hostingEnv;
        public Services(IJwtAuthenticationManager jwtAuthenticationManager, IConfiguration configuration, IHostingEnvironment ev)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            _configuration = configuration;
            _hostingEnv = ev;
        }

        [AllowAnonymous]
        [Route("requesttoken")]
        [HttpPost]
        public IActionResult MyToken([FromBody] JObject R_PRM)// JsonElement R_PRM)
        {
            var returnObject = new object();
            try
            {
                var data = R_PRM["data"];
                returnObject = this.GetToken(data.ToString());
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return Ok(returnObject);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] JObject R_PRM)
        {
            try
            {
                var token = jwtAuthenticationManager.Authenticate(R_PRM, _configuration);
                if (token == null)
                    return Unauthorized();
                return Ok(token);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("MyData")]
        [HttpPost]
        public IActionResult MyDataCall([FromBody] JObject R_PRM)// JsonElement R_PRM)
        {

            try
            {
                var returnObject = new object();
                //var actionID = R_PRM.GetProperty("actionID").GetString().ToLower();
                //var data = R_PRM.GetProperty("data").GetString();
                var actionID = R_PRM["eventID"].ToString().ToLower();
                var data = R_PRM["data"];

                switch (actionID)
                {
                    case "token":
                        returnObject = GetToken(data.ToString());
                        break;
                    /*========================APP START ======================================================*/

                    case "getdata":
                        returnObject = getlist(R_PRM, actionID);
                        break;

                        /*========================APP END ======================================================*/

                }
                return Ok(returnObject);


            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }

        [Authorize]
        [Route("MyAuthenticatedData")]
        [HttpPost]
        public IActionResult MyAuthenticatedDataCall([FromBody] JObject R_PRM)// JsonElement R_PRM)
        {
            try
            {
                var returnObject = new object();
                var actionID = R_PRM["eventID"].ToString().ToLower();
                var data = R_PRM["data"];

                switch (actionID)
                {
                    case "token":
                        returnObject = GetToken(data.ToString());
                        break;
                    case "setgallerygroup":
                         returnObject = getlist(R_PRM, actionID);
                        break;
                    case "setstudent":
                        returnObject = setstudent(R_PRM);
                        break;
                    case "setcountry":
                        returnObject = setcountry(R_PRM);
                        break;


                }
                return Ok(returnObject);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }



        /*========================================Master entries===============================================================*/



        private object setcountry(JObject data)
        {
            object NewToken = new object();
            List<object> _list = new List<object>();
            try
            {

                object objMember = data["data"];
                JObject joMember = (JObject)objMember;

                JObject NewData = new JObject();

                object objSHA = data["SHA"];

                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var varcountry = data["data"].ToObject<MCountry>();
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    using (var transaction = dbcontext.Database.BeginTransaction())
                    {
                        try
                        {


                            List<LoginRequestAttempt> userLoginRequestAttempts = (from s in dbcontext.LoginRequestAttempt where s.ClientId == data["SHA"]["clientid"].ToString() && s.KeyToAuthenticate == data["SHA"]["token"].ToString() select s).ToList();
                            if (userLoginRequestAttempts.Count == 0)
                                throw new Exception("Invalid Access");
                            userLoginRequestAttempts[0].IsActive = 0;
                            userLoginRequestAttempts[0].AttemptUser = "New Country";
                            userLoginRequestAttempts[0].UseTime = System.DateTime.Now;
                            dbcontext.MCountry.Add(varcountry);
                            dbcontext.SaveChanges();
                            transaction.Commit();

                        }
                        catch
                        {
                            throw;
                        }


                    }

                }
                NewToken = (GetToken(data["SHA"]["clientid"].ToString()));
            }
            catch { throw; }


            Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
            {
                { "Save", true },

            };
            o.Merge((JObject)NewToken);

            return (o, _list);

        }



        private object setstudent(JObject data)
        {
            object NewToken = new object();
            List<object> _list = new List<object>();
            try
            {

                //object objMember = data["data"]["Member"];
                object objMember = data["data"];
                JObject joMember = (JObject)objMember;

                JObject NewData = new JObject();

                object objSHA = data["SHA"];

                NewData["data"] = joMember;
                NewData["SHA"] = (JObject)objSHA;
                Common.ValidDatePostedData(NewData, "data");
                var varMember = data["data"].ToObject<EAdmDetails>();
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    using (var transaction = dbcontext.Database.BeginTransaction())
                    {
                        try
                        {


                            List<LoginRequestAttempt> userLoginRequestAttempts = (from s in dbcontext.LoginRequestAttempt where s.ClientId == data["SHA"]["clientid"].ToString() && s.KeyToAuthenticate == data["SHA"]["token"].ToString() select s).ToList();
                            if (userLoginRequestAttempts.Count == 0)
                                throw new Exception("Invalid Access");
                            userLoginRequestAttempts[0].IsActive = 0;
                            userLoginRequestAttempts[0].AttemptUser = "New Reg";
                            userLoginRequestAttempts[0].UseTime = System.DateTime.Now;
                            varMember.ApplicationDate = System.DateTime.Now;
                            dbcontext.EAdmDetails.Add(varMember);
                            _list = (from pro in dbcontext.EAdmDetails select pro).Take(10).ToList<object>();
                            dbcontext.SaveChanges();
                            transaction.Commit();

                        }
                        catch
                        {
                            throw;
                        }


                    }

                }
                NewToken = (GetToken(data["SHA"]["clientid"].ToString()));
            }
            catch { throw; }


            Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
            {
                { "Save", true },

            };
            o.Merge((JObject)NewToken);

            return (o,_list);

        }



        private object getlist(object R_PRM, string eventID)
        {
            string r_code = "Success";
            string r_data = "";

            List<object> _list = new List<object>();
            try
            {

                using (mararkContext dtoContext = AppSettingValue.GetConnectionObject(_configuration))
                {
                    _list = (from pro in dtoContext.EAdmDetails
                             select pro).Take(10).ToList<object>();

                }
            }
            catch (Exception s)
            {
                r_code = s.Message;

            }
            //return (Status, _list);
            return (new { r_code, r_data = R_PRM, _list, eventID });
        }


        #region Generate New Token
        private object GetToken(string ClientID)
        {


            string RandomData = Common.RandomString(4);
            LoginRequestAttempt obj = new LoginRequestAttempt();
            try
            {
                // using (bpr_IGICContext dbcontext =  new bpr_IGICContext())
                using (mararkContext dbcontext = AppSettingValue.GetConnectionObject(_configuration))
                //using (bpr_IGICContext dbcontext = new bpr_IGICContext(options => options.UseMySQL(Configuration.GetConnectionString("IGICConn")))
                {
                    List<LoginRequestAttempt> objul = new List<LoginRequestAttempt>();
                    objul = (from s in dbcontext.LoginRequestAttempt where s.ClientId == ClientID && s.IsActive.Value == 1 select s).ToList<LoginRequestAttempt>();
                    foreach (LoginRequestAttempt obju in objul)
                    {
                        obju.IsActive = 0;
                        obju.AttemptUser = "False / Retry";
                        obju.UseTime = System.DateTime.Now;
                    }

                    obj.ClientId = ClientID;
                    obj.KeyToAuthenticate = RandomData;
                    obj.CreateTime = System.DateTime.Now;
                    obj.IsActive = 1;
                    dbcontext.LoginRequestAttempt.Add(obj);
                    dbcontext.SaveChangesAsync();

                }
            }
            catch
            {
                throw;
            }
            Newtonsoft.Json.Linq.JObject o = new Newtonsoft.Json.Linq.JObject
            {
                { "Token", RandomData }
            };


            return o;

        }
        #endregion


    }
}
