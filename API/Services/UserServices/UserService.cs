using API.Models.UserModels;
using API.Response.UserResponses;

namespace API.Services.UserServices
{
    public class UserService : IUserService
    {
        public LoginUserResponse GetLoginUserResponse(User user, UserCredentials userCredentials)
        {
           LoginUserResponse loginUserResponse = new()
            {
                UserName = userCredentials.UserName,
                FirstName = user.FirstName,
                IsOwnerOperator = user.IsOwnerOperator,
            };
            return loginUserResponse;
        }
    }
}
