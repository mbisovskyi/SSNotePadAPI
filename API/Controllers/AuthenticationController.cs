using API.Database;
using API.Models.UserModels;
using API.Requests.AuthenticationRequests;
using API.Services.AuthenticationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : AppController
    {
        private readonly DatabaseDbContext dbContext;
        private readonly IAuthenticationService authService;

        public AuthenticationController(DatabaseDbContext context, IAuthenticationService service)
        {
            dbContext = context;
            authService = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser_Async(LoginUserRequest loginRequest)
        {
            if(loginRequest.IsPasswordConfirmed) 
            {
                User? foundUser = await dbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(loginRequest.UserName, StringComparison.CurrentCultureIgnoreCase) &&
                user.Password.Equals(loginRequest.Password, StringComparison.CurrentCultureIgnoreCase));

                if (foundUser is not null)
                {
                    foundUser.UserName = loginRequest.UserName;
                    return Ok(authService.GetLoginUserResponse(foundUser));
                }

                return BadRequest("Wrong credentials");
            }

            return BadRequest("Password is not confirmed.");
        }

        [HttpPost("NewUser")]
        public async Task<IActionResult> CreateNewUser_Async(CreateUserRequest request)
        {
            if (request.IsPasswordConfirmed)
            {
                User? user = await dbContext.Users.FirstOrDefaultAsync(user => user.UserName.Equals(request.UserName, StringComparison.CurrentCultureIgnoreCase) ||
                user.Email.Equals(request.Email,StringComparison.CurrentCultureIgnoreCase));
                if(user == null) /* Returns true if credentials are unique */
                {
                    user = authService.NewUserRequestToUserModel(request);
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    return CreatedAtRoute(request, authService.GetNewUserResponse(user));
                }
                return BadRequest("Given credentials are not unique.");
            }
            return BadRequest("Password was not confirmed.");
        }
    }
}
