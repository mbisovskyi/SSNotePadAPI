using API.Models.UserModels;
using API.Response.UserResponses;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public LoginUserResponse GetLoginUserResponse(User user, UserCredentials userCredentials);

        public string GenerateToken();
    } 
}
