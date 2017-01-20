using Microsoft.AspNet.Identity.EntityFramework;
using Slingshot_v2.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<VCard> tblVCards { get; set; }

        public DbSet<Recipient> tblRecipients { get; set; }

        public DbSet<Campaign> tblCampaigns { get; set; }
        public DbSet<Email> tblEmails { get; set; }
        public DbSet<Attachment> tblAttachments { get; set; }

        public DbSet<UserCampaign> tblUserCampaigns { get; set; }

        public DbSet<Image> tblImages { get; set; }

        public DbSet<History> tblHistory { get; set; }

        public DbSet<Event> tblEvents { get; set; }


        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
