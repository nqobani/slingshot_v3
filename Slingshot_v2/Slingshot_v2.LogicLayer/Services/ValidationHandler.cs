using Slingshot_v2.Data;
using Slingshot_v2.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.LogicLayer.Services
{
    public class ValidationHandler
    {
        private ApplicationDbContext con = new ApplicationDbContext();
        private DbConnection dbCon = new DbConnection();

        public Boolean UserCampaignValidation(string useId, long campId)
        {
            var uc = con.tblUserCampaigns.FirstOrDefault(s => s.userId.Equals(useId) && s.campaignId == campId);

            Boolean hasAccess = false;

            if (uc.campaignId == campId && useId == uc.userId)
            {
                hasAccess = true;
            }
            return hasAccess;
        }

        public SendGrid.Helpers.Mail.Attachment GetAttechmentData(string filePath)
        {
            if (filePath.Substring(0, 8).ToLower().Contains("https"))
            {
                string base64ImageRepresentation = ConvertImageURLToBase64(filePath);
                string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
                string type = filePath.Substring(filePath.LastIndexOf('.') + 1);

                return new SendGrid.Helpers.Mail.Attachment
                {
                    Filename = fileName,
                    Type = type,
                    Disposition = "inline",
                    ContentId = "kjhlknmnjhjkk",
                    Content = base64ImageRepresentation
                };
            }
            else
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                string type = filePath.Substring(filePath.LastIndexOf('.') + 1);

                return new SendGrid.Helpers.Mail.Attachment
                {
                    Filename = fileName,
                    Type = type,
                    Disposition = "inline",
                    ContentId = "kjhlknmnjhjkk",
                    Content = base64ImageRepresentation
                };
            }

        }

        public Boolean IsCraetor(string userId, long campaignId)
        {
            Boolean isCreator = false;
            var camp = con.tblCampaigns.SingleOrDefault(c => c.creatorId.Equals(userId) && c.Id == campaignId);
            if (camp.Id == campaignId && camp.creatorId == userId)
            {
                isCreator = true;
            }
            return isCreator;
        }
        public Boolean CanUserShare(string userId, long campaignId)
        {
            Boolean share = false;
            string userType = (userId);
            if (userType.ToLower().Equals("admin"))
            {
                share = true;
            }
            else
            {
                share = IsCraetor(userId, campaignId);
            }
            return share;
        }
        public Boolean AlreadyShared(string userId, long campaignId)
        {
            Boolean shared = false;
            var userCamp = con.tblUserCampaigns.FirstOrDefault(uc => uc.campaignId == campaignId && uc.userId.Equals(userId));
            if (userCamp != null)
            {
                shared = true;
            }
            return shared;
        }



        public String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();

            Byte[] _byte = GetImage(url);

            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

            return _sb.ToString();
        }

        public byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;
            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
    }
}
