using Slingshot_v2.Data.Models;
using Slingshot_v2.LogicLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Slingshot_v2.Controllers
{
    [RoutePrefix("api/campaign")]
    public class CampaignController : ApiController
    {
        UserService obj = new UserService();
        [Route("send")]
        public History sendCampaigns(string userId, long vcardId, long campId, string toEmail)
        {
            return obj.sendCampaign(userId, vcardId, campId, toEmail);
        }
        [Route("add")]
        public Campaign addCampaign(string creatorId, string attechmentsJSONString, string campaignName = "No Name", Boolean prefared = false, string thumbnail = "HTTPS", string subject = "TESTIING", string HTML = "<!DOCTYPE html>", string status = "public")
        {
            UserService obj = new UserService();
            return obj.createCampaign(creatorId, campaignName, prefared, thumbnail, subject, HTML, attechmentsJSONString, status);
        }
        [Route("get")]
        public IEnumerable<Campaign> getCampaigns(string userId, string name = "")
        {
            UserService obj = new UserService();
            return obj.getCampaigns(userId, name);
        }
        [Route("share")]
        public Boolean shareCampaign(string userId, long campaignId)
        {
            return obj.ShareCampaigns(userId, campaignId);
        }
        [Route("uploadImage")]
        public string uploadImage()
        {
            //Directory.CreateDirectory(@"C:\Users\User\Music\images");
            //string sourceFile = Path.Combine(@"C:\Users\User\Music\", "banner.jpg");
            //string destFile = Path.Combine(@"C:\Users\User\Music\images\", "banner.jpg");
            //File.Copy(sourceFile, destFile, true);
            return Directory.GetCurrentDirectory().ToString();
        }
    }
}
