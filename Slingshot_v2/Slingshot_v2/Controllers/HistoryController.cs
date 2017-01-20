using Slingshot_v2.Data.Models;
using Slingshot_v2.LogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Slingshot_v2.Controllers
{
    [RoutePrefix("api/history")]
    public class HistoryController : ApiController
    {
        UserService obj = new UserService();
        [Route("getuserhistory")]
        public IEnumerable<History> GetUserHistory(string userId)
        {
            return obj.GetUserHistory(userId);
        }
    }
}
