using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VNH.BE.API.Infrastructure.Models;
using VNH.BE.Domain.Aggregates.Identity;

namespace VNH.BE.API.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<ApplicationUser> Signin(string userName, string password);

        string CreateToken(ApplicationUser user);
    }
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenicateModel _authenicateModel;
        public AccountService(UserManager<ApplicationUser> userManager,
            IOptions<AuthenicateModel> option)
        {
            _userManager = userManager;

            _authenicateModel = option.Value;
        }

        public async Task<ApplicationUser> Signin(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            if(result)
            {
                return user;
            }

            return null;
        }

        public string CreateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_authenicateModel.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim("IsAdmin", user.IsAdmin.ToString()),
                    new Claim("Permission", "all"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_authenicateModel.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256),
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
