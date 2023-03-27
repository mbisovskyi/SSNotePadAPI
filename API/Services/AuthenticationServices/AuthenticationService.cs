using API.Models.UserModels;
using API.Response.UserResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration config;
        public AuthenticationService (IConfiguration _config)
        {
            config = _config;
        }
        public LoginUserResponse GetLoginUserResponse(User user, UserCredentials userCredentials)
        {
           LoginUserResponse loginUserResponse = new()
            {
                UserName = userCredentials.UserName,
                FirstName = user.FirstName,
                IsOwnerOperator = user.IsOwnerOperator,
                Token = GenerateToken(),
            };
            return loginUserResponse;
        }

        public bool ValidateNewUserCredentials(User newUser, DbSet<UserCredentials> dbCredentials)
        {
            UserCredentials? existedCredentials = dbCredentials.FirstOrDefault(credentials => 
            credentials.UserName.Equals(newUser.Credentials.UserName, StringComparison.CurrentCultureIgnoreCase) ||
            credentials.Email.Equals(newUser.Credentials.Email, StringComparison.CurrentCultureIgnoreCase));
            return existedCredentials == null;
        }

        public string GenerateToken() 
        {
            var token = new JwtSecurityToken
                   (
                       issuer: config["Jwt:Issuer"],
                       audience: config["Jwt:Audience"],
                       expires: DateTime.UtcNow.AddDays(1),
                       notBefore: DateTime.UtcNow,
                       signingCredentials: new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                           SecurityAlgorithms.HmacSha256)
                   );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }


    }
}
