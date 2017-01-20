using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data.Models
{
    public class VCard
    {
        [Key]
        public long Id { get; set; }
        public string userId { get; set; }
        public string profileImage { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string company { get; set; }
        public string jobTitle { get; set; }
        public string email { get; set; }
        public string twitter { get; set; }
        public string webPageAddress { get; set; }
        public string businessPhoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string code { get; set; }
    }
}
