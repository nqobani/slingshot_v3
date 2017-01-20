using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data.Models
{
    public class Campaign
    {
        [Key]
        public long Id { get; set; }
        public string creatorId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public Boolean prefared { get; set; }
        public string thumbnail { get; set; }
        public ISet<Email> email { get; set; }
    }
}
