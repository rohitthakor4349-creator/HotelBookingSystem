using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBookingSystem.Helper
{
    public interface IJWTTokenHelper
    {
        string JwtTokenGenerate(string Role, string Email, int UserId);
    }
    public class JWTTokenHelper : IJWTTokenHelper
    {
        private readonly IConfiguration configuration;

        public JWTTokenHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string JwtTokenGenerate(string Role, string Email, int UserId)
        {
            var Key = configuration["jwt:Key"];

            if (string.IsNullOrEmpty(Key))
            {
                throw new Exception("jwt Key Missing");

            }
            var Securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credential = new SigningCredentials(Securitykey, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(ClaimTypes.Email,Email),
                new Claim(ClaimTypes.Role,Role),
                new Claim(ClaimTypes.NameIdentifier,UserId.ToString())


            };
            var token = new JwtSecurityToken(

                issuer: configuration["jwt:Issuer"],
                audience: configuration["jwt:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(configuration["jwt:Expiredays"])),
                signingCredentials: credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
