using API.Models.UserModels;
using API.Requests.AuthenticationRequests;
using API.Response.AuthenticationResponses;
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

        // -- RESPONSE Generators ---------------------------------------------------------------------------------------------------------
        public LoginUserResponse GetLoginUserResponse(User user)
        {
           LoginUserResponse loginUserResponse = new()
            {
                UserId = user.Id,
                UserName = user.Credentials.UserName,
                FirstName = user.FirstName,
                IsOwnerOperator = user.IsOwnerOperator,
                Token = GenerateToken(),
            };
            return loginUserResponse;
        }

        public NewUserResponse GetNewUserResponse(User newUser)
        {
            NewUserResponse newUserResponse = new()
            {
                UserId = newUser.Id,
                UserName = newUser.Credentials.UserName,
                Email = newUser.Credentials.Email,
                IsOwnerOperator = newUser.IsOwnerOperator,
            };
            return newUserResponse;
        }

        // -- From REQUEST Formatters ----------------------------------------------------------------------------------------------------

        public User NewUserRequestToUserModel(CreateUserRequest newUserRequest)
        {
            User user = new User()
            {
                FirstName = newUserRequest.FirstName,
                Credentials = new UserCredentials()
                {
                    UserName = newUserRequest.UserName,
                    Password = newUserRequest.Password,
                    Email = newUserRequest.Email.ToString(),
                }
            };
            return user;
        }

        // -- OTHER Methods ----------------------------------------------------------------------------------------------------------------
        public bool ValidateNewUserCredentials(CreateUserRequest newUserRequest, DbSet<UserCredentials> dbCredentials)
        {
            UserCredentials? existedCredentials = dbCredentials.FirstOrDefault(credentials =>
            credentials.UserName.Equals(newUserRequest.UserName, StringComparison.CurrentCultureIgnoreCase) ||
            credentials.Email.Equals(newUserRequest.Email, StringComparison.CurrentCultureIgnoreCase));
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
