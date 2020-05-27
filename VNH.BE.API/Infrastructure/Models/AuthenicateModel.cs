using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VNH.BE.API.Infrastructure.Models
{
    public class AuthenicateModel
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
