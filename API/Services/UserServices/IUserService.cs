using API.Models.UserModels;
using API.Response.UserResponses;

namespace API.Services.UserServices
{
    public interface IUserService
    {
        public LoginUserResponse GetLoginUserResponse(User user, UserCredentials userCredentials);
    } 
}
