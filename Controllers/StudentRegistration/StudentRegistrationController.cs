using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MT.Controllers.Interface;
using MT.Services.Interface;
using MT.Services.StudentRegistration;
using Newtonsoft.Json.Linq;
using System;

namespace MT.Controllers.StudentRegistration
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRegistrationController : ControllerBase, IController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(StudentRegistrationController));
        protected readonly IService _IService;
        protected readonly IConfiguration _configuration;

        public StudentRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _IService = new StudentRegistrationService(_configuration);
        }


        [Authorize]
        [Route("ProcessData")]
        [HttpPost]
        public IActionResult ProcessData([FromBody] JObject R_PRM)
        {
            log.Debug($"Student Registration Process with params: {R_PRM} starts");
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

                log.Debug($"Student Registration process with params: {R_PRM} and Return object: {returnObject} ends");
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
