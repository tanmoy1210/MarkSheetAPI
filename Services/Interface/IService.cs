using MT.DBLayer;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MT.Services.Interface
{
    public interface IService
    {
        object SaveOrUpdate(JObject data, string action);
        object GetObjectList(JObject R_PRM);
        object GetObject(JObject R_PRM);

    }

    public enum RCode
    {
        Success,
        Fail
    }
}
