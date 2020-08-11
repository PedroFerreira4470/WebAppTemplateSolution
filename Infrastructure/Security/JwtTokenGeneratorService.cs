using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public class JwtTokenGeneratorService : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JwtTokenGeneratorService(IConfiguration config, UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("userId",user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null)
                    continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(4),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
