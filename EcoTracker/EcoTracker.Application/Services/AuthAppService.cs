using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcoTracker.Application.Services
{
    public class AuthAppService : ApplicationService, IAuthAppService
    {
        private readonly IConfiguration _configuration;

        private readonly IUserDomainService _userDomainService;

        public AuthAppService(INotificationManager notificationManager, IMapper mapper, IConfiguration configuration, IUserDomainService userDomainService) : base(notificationManager, mapper)
        {
            _configuration = configuration;
            _userDomainService = userDomainService;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT:ApiSecret")!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userDomainService.GetByEmailAsync(email);

            if (user == null || user?.Password != password)
                return string.Empty;

            return this.GenerateToken(user);
        }
    }
}
