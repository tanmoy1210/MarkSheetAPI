using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MT.Controllers.Interface;
using MT.Services.Interface;
using MT.Services.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MT.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase, IController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserRoleController));
        protected readonly IService _IService;
        protected readonly IConfiguration _configuration;
        public UserRoleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _IService = new UserRoleCreationService(_configuration);
        }

        [Authorize]
        [Route("ProcessData")]
        [HttpPost]
        public IActionResult ProcessData([FromBody] JObject R_PRM)
        {
            log.Debug($"UserRoleCreation with params: {R_PRM} starts");
            try
            {
                var returnObject = new object();
                var action = R_PRM["eventID"].ToString().ToUpper();
                var data = R_PRM["data"];

                switch (action)
                {
                    case "SAVE":
                        returnObject = _IService.SaveOrUpdate(R_PRM, action);
                        break;
                    case "GET":
                        returnObject = _IService.GetObject(R_PRM);
                        break;
                    case "GETALL":
                        returnObject = _IService.GetObjectList(R_PRM);
                        break;
                    case "UPDATE":
                        returnObject = _IService.SaveOrUpdate(R_PRM, action);
                        break;
                    case "DELETE":
                        returnObject = _IService.SaveOrUpdate(R_PRM, action);
                        break;
                }

                log.Debug($"UserRoleCreation process with params: {R_PRM} and Return object: {returnObject} ends");
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
