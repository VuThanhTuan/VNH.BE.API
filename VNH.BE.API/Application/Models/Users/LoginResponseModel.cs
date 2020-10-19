using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VNH.BE.API.Application.Models.Users
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
