using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slingshot_v2.Data.Models
{
    public class Image
    {
        [Key]
        public long Id { get; set; }
        public string imagPath { get; set; }
        public DateTime captureDateTime { get; set; }
    }
}
