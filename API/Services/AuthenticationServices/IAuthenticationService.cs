using API.Models.UserModels;
using API.Response.UserResponses;
using API.Requests.AuthenticationRequests;
using API.Response.AuthenticationResponses;

namespace API.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public LoginUserResponse GetLoginUserResponse(User user);
        public NewUserResponse GetNewUserResponse(User newUser);
        public User NewUserRequestToUserModel(CreateUserRequest newUserRequest);
        public string GenerateToken();
    } 
}
