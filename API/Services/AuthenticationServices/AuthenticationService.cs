using API.Models.UserModels;
using API.Requests.AuthenticationRequests;
using API.Response.AuthenticationResponses;
using API.Response.UserResponses;
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
                UserName = user.UserName,
                Token = GenerateToken(),
            };
            return loginUserResponse;
        }

        public NewUserResponse GetNewUserResponse(User newUser)
        {
            NewUserResponse newUserResponse = new()
            {
                UserId = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
            };
            return newUserResponse;
        }

        // -- From REQUEST Formatters ----------------------------------------------------------------------------------------------------

        public User NewUserRequestToUserModel(CreateUserRequest newUserRequest)
        {
            User user = new User(){
              UserName = newUserRequest.UserName,
              Password = newUserRequest.Password,
              Email = newUserRequest.Email,
            };
            return user;
        }

        // -- OTHER Methods ----------------------------------------------------------------------------------------------------------------
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
