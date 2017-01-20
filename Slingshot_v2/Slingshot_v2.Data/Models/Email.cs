using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data.Models
{
    public class Email
    {
        [Key]
        public long Id { get; set; }
        public long campaignId { get; set; }
        public string subject { get; set; }
        public string html { get; set; }
        public ISet<Attachment> attachments { get; set; }
    }
}
