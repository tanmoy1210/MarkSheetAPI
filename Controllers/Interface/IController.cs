using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MT.Controllers.Interface
{
    public interface IController
    {
        IActionResult ProcessData([FromBody] JObject R_PRM);
    }
}
