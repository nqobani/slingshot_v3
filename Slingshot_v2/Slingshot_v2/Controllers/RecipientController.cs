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
    [RoutePrefix("api/recipient")]
    public class RecipientController : ApiController
    {
        UserService obj = new UserService();
        [Route("addclient")]
        public Recipient AddClient(string userId, string fName, string lName, string email, string cell, string jobTile, string country, string province, string city, string street, string code)
        {
            return obj.CaptureRecipient(userId, fName, lName, email, cell, jobTile, country, province, city, street, code);
        }

        [Route("getUserClients")]
        public IEnumerable<Recipient> GetAllUserClients(long userId)
        {
            return obj.GetAllUserRecipients(userId);
        }
        [Route("getClient")]
        public Recipient GetClient(long userId)
        {
            return obj.GetRecipient(userId);
        }

    }
}
