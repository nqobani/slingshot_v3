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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UserService obj = new UserService();
        [Route("registerUser")]
        public User register(string userName, string firstName, string lastName, string email, string password, string type = "member")
        {
            return obj.createUser(userName, firstName, lastName, email, password, type);
        }
        [Route("get")]
        public UserModel[] GetAllUsers(string userName = "")
        {
            return obj.GetAllUsers(userName);
        }
        [Route("createVCard")]
        public Data.Models.VCard createVCard(string userId, string firstName, string lastName, string company, string jobTitle, string email, string webPageAddress, string twitter, string businessPhoneNumber, string mobilePhone, string country, string city, string cityCode, string imageLink)
        {
            return obj.CreateVCard(userId, firstName, lastName, company, jobTitle, email, webPageAddress, twitter, businessPhoneNumber, mobilePhone, country, city, cityCode, imageLink);
        }
        [Route("getvcards")]
        public IEnumerable<Data.Models.VCard> GetUserVCards(string userId)
        {
            return obj.GetUserVCards(userId);
        }
    }
}
