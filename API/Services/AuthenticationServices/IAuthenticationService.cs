using API.Models.UserModels;
using API.Response.UserResponses;
using API.Requests.AuthenticationRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Response.AuthenticationResponses;

namespace API.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public LoginUserResponse GetLoginUserResponse(User user);
        public NewUserResponse GetNewUserResponse(User newUser);
        public User NewUserRequestToUserModel(CreateUserRequest newUserRequest);

        public bool ValidateNewUserCredentials(CreateUserRequest newUserRequest, DbSet<UserCredentials> dbCredentials);
        public string GenerateToken();
    } 
}
