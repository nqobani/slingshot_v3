using Slingshot_v2.Data.EntityFramework;
using Slingshot_v2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data
{
    public class DbConnection
    {
        ApplicationDbContext dbCon = new ApplicationDbContext();

        public User createUser(string userName, string firstName, string lastName, string email, string password, string type)
        {

            User newUser = new User();
            newUser.Email = email;
            newUser.PasswordHash = password;
            newUser.type = type;
            newUser.UserName = userName;
            newUser.FirstName = firstName;
            newUser.LastName = lastName;
            dbCon.Users.Add(newUser);

            dbCon.SaveChanges();
            var userId = newUser.Id;

            User user = dbCon.Users.SingleOrDefault(u => u.Id == userId);
            return user;
        }
        public VCard createVCard(string userId, string firstName, string lastName, string company, string jobTitle, string email, string webPageAddress, string twitter, string businessPhoneNumber, string mobilePhone, string country, string city, string cityCode, string imageLink)
        {
            VCard newVCard = new VCard();
            newVCard.userId = userId;
            newVCard.firstName = firstName;
            newVCard.lastName = lastName;
            newVCard.company = company;
            newVCard.jobTitle = jobTitle;
            newVCard.email = email;
            newVCard.webPageAddress = webPageAddress;
            newVCard.twitter = twitter;
            newVCard.businessPhoneNumber = businessPhoneNumber;
            newVCard.mobileNumber = mobilePhone;
            newVCard.country = country;
            newVCard.city = city;
            newVCard.code = cityCode;
            newVCard.profileImage = imageLink;

            dbCon.tblVCards.Add(newVCard);
            dbCon.SaveChanges();

            long vCardId = newVCard.Id;

            VCard vcard = dbCon.tblVCards.FirstOrDefault(v => v.Id == vCardId);
            return vcard;

        }
        public void userCampaign(string userId, long campaignId)
        {
            UserCampaign usercampaign = new UserCampaign();
            usercampaign.campaignId = campaignId;
            usercampaign.userId = userId;

            dbCon.tblUserCampaigns.Add(usercampaign);
            dbCon.SaveChanges();
        }

        public Boolean UserExists(string userId)
        {
            Boolean userExists = false;
            var user = dbCon.Users.FirstOrDefault(u => u.Id.Equals(userId));
            if (user != null)
            {
                userExists = true;
            }
            return userExists;
        }
        public string GetUserType(string userId)
        {
            var user = dbCon.Users.FirstOrDefault(u => u.Id.Equals(userId));
            string userType = user.type;
            return userType;
        }
        public UserModel[] GetAllUsers(string userName)
        {
            var user = dbCon.Users.Where(u => u.UserName.Contains(userName)).ToList();
            UserModel[] users = new UserModel[user.Count()];
            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new UserModel
                {
                    email = user[i].Email,
                    firstName = user[i].FirstName,
                    lastName = user[i].LastName,
                    phoneNumber = user[i].PhoneNumber
                };
            }
            return users;
        }

        public Recipient CaptureRecipientData(string userId, string fName, string lName, string email, string cell, string jobTile, string country, string province, string city, string street, string code)
        {
            var recipient = new Recipient
            {
                userId = userId,
                firstName = fName,
                lastName = lName,
                email = email,
                cell = cell,
                jobTitle = jobTile,
                country = country,
                province = province,
                city = city,
                street = street,
                code = code
            };
            dbCon.tblRecipients.Add(recipient);
            dbCon.SaveChanges();

            return recipient;
        }





        public Campaign createCampaign(string creatorId, string name, Boolean prefared, string thumbnail, string status = "public")
        {
            Campaign newCampaign = new Campaign();
            newCampaign.creatorId = creatorId;
            newCampaign.name = name;
            newCampaign.thumbnail = thumbnail;
            newCampaign.status = status;
            newCampaign.prefared = prefared;
            dbCon.tblCampaigns.Add(newCampaign);
            dbCon.SaveChanges();

            long campaignId = newCampaign.Id;
            Campaign campaign = dbCon.tblCampaigns.SingleOrDefault(c => c.Id == campaignId);
            return campaign;
        }
        public Email createEmail(long campaignId, string subject, string HTML)
        {
            Email email = new Email();
            email.campaignId = campaignId;
            email.subject = subject;
            email.html = HTML;

            dbCon.tblEmails.Add(email);
            dbCon.SaveChanges();

            long emailId = email.Id;
            Email mail = dbCon.tblEmails.FirstOrDefault(em => em.Id == emailId);
            return mail;
        }
        public Attachment createAttachment(long emailId, string name, string file/*Path*/)
        {
            Attachment newAttechment = new Attachment();
            newAttechment.emailId = emailId;
            newAttechment.name = name;
            newAttechment.file = file;
            dbCon.tblAttachments.Add(newAttechment);
            dbCon.SaveChanges();

            long attId = newAttechment.Id;
            Attachment attachment = dbCon.tblAttachments.SingleOrDefault(a => a.Id == attId);
            return attachment;
        }

        public Event createEvent(string creatorId, string title, string location, DateTime startDateTime, DateTime endDateTime)
        {
            Event newEvent = new Event();
            newEvent.title = title;
            //newEvent.location = location;
            newEvent.startDateTime = startDateTime;
            newEvent.endDateTime = endDateTime;
            //newEvent.CreatorId = creatorId;
            dbCon.tblEvents.Add(newEvent);
            dbCon.SaveChanges();
            return newEvent;
        }

        public string GetUserEmail(string userId)
        {
            User user = dbCon.Users.FirstOrDefault(s => s.Id == userId);
            string email = user.Email;
            return email;
        }
        public VCard GetVCard(long vCardId)
        {
            VCard vcard = dbCon.tblVCards.FirstOrDefault(v => v.Id == vCardId);
            return vcard;
        }
        public Email GetEmail(long campId)
        {
            var email = dbCon.tblEmails.FirstOrDefault(e => e.campaignId == campId);
            return email;
        }

        public IEnumerable<Recipient> GetAllUserRecipients(long userId)
        {
            var recipients = dbCon.tblRecipients.Where(r => r.userId.Equals(userId));
            return recipients;
        }
        public Recipient GetRecipient(long recipId)
        {
            var recipient = dbCon.tblRecipients.FirstOrDefault(r => r.Id == recipId);
            return recipient;
        }


        public IEnumerable<Campaign> getAllCampaigns(string userId, string campName)
        {
            var camps = dbCon.tblCampaigns.Where(c => c.creatorId.Equals(userId) || c.status.ToLower().Contains("public"));
            return camps;
        }
        public IEnumerable<Attachment> GetAttachmentByEmailId(long emailId)
        {
            var atts = dbCon.tblAttachments.Where(s => s.emailId == emailId);
            return atts;
        }
        public IEnumerable<History> GetUserHistory(string userId)
        {
            var history = dbCon.tblHistory.Where(h => h.userId.Equals(userId));
            return history;
        }
        public IEnumerable<VCard> GetVCards(string userId)
        {
            var vCards = dbCon.tblVCards.Where(u => u.userId.Equals(userId));
            return vCards;
        }

        public History createHistory(string userId, long campaignId, string toEMail, long imageId = 0)
        {
            History newHistory = new History();
            newHistory.userId = userId;
            newHistory.imageId = imageId;
            newHistory.campaignId = campaignId;
            newHistory.toEmail = toEMail;
            newHistory.sentDateTime = DateTime.Now;

            dbCon.tblHistory.Add(newHistory);
            dbCon.SaveChanges();

            long histId = newHistory.Id;
            History history = dbCon.tblHistory.FirstOrDefault(h => h.Id == histId);
            return history;
        }

        ///UPDATE Objects in database tables///UPDATE Objects in database tables///UPDATE Objects in database tables///UPDATE Objects in database tables
        //////UPDATE Objects in database tables///UPDATE Objects in database tables///UPDATE Objects in database tables///UPDATE Objects in database tables

        public Email UpdateEmail(long emailId, string subject, string html)
        {
            var email = dbCon.tblEmails.FirstOrDefault(e => e.Id == emailId);

            if (email != null)
            {
                email.subject = subject;
                email.html = html;
            }
            dbCon.SaveChanges();
            return email;
        }


        ///DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA//
        //////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA/////DELETION///RED AREA//

        ///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS
        //////DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS///DELETE USERS and VCARDS
        public User DeleteUser(string userId)
        {
            var user = dbCon.Users.FirstOrDefault(u => u.Id == userId);
            dbCon.Users.Remove(user);
            dbCon.SaveChanges();
            return user;
        }
        public VCard DeleteUserVCard(long vCardId)
        {
            var vCard = dbCon.tblVCards.FirstOrDefault(v => v.Id == vCardId);
            dbCon.tblVCards.Remove(vCard);
            dbCon.SaveChanges();
            return vCard;
        }
        public IEnumerable<VCard> DeleteVcardsByUserId(long userId)
        {
            var vCards = dbCon.tblVCards.Where(v => v.userId.Equals(userId));
            dbCon.tblVCards.RemoveRange(vCards);
            dbCon.SaveChanges();
            return vCards;
        }

        ///Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment//
        ////Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment/////Delete campaign>>email>>attechment//
        public Campaign DeleteCampaign(long campId)
        {
            var camp = dbCon.tblCampaigns.FirstOrDefault(c => c.Id == campId);
            dbCon.tblCampaigns.Remove(camp);
            dbCon.SaveChanges();
            return camp;
        }
        public Email DeleteEmail(long emialId)
        {
            var email = dbCon.tblEmails.FirstOrDefault(e => e.Id == emialId);
            dbCon.tblEmails.Remove(email);
            dbCon.SaveChanges();
            return email;
        }
        public Attachment DeleteAttechment(long attId)
        {
            var attachment = dbCon.tblAttachments.FirstOrDefault(a => a.Id == attId);
            dbCon.tblAttachments.Remove(attachment);
            dbCon.SaveChanges();
            return attachment;
        }
        public IEnumerable<Attachment> DeleteAttectmentByEmailId(long emialId)
        {
            var attachment = dbCon.tblAttachments.Where(a => a.emailId == emialId);
            dbCon.tblAttachments.RemoveRange(attachment);
            dbCon.SaveChanges();
            return attachment;
        }
    }
}
