using API.Models.UserModels;
using API.Response.UserResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public LoginUserResponse GetLoginUserResponse(User user, UserCredentials userCredentials);
        public bool ValidateNewUserCredentials(User newUser, DbSet<UserCredentials> dbCredentials);
        public string GenerateToken();
    } 
}
