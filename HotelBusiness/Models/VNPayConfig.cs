using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBusiness.Models
{
    public class VNPayConfig
    {
        public string Version { get; set; }
        public string TmnCode { get; set; }
        public string HashSecret { get; set; }
        public string Url { get; set; }
        public string ReturnUrl { get; set; }
        public string Command { get; set; }
    }
}
