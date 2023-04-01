using API.Database;
using API.Models.UserModels;
using API.Requests.AuthenticationRequests;
using API.Response.AuthenticationResponses;
using API.Services.AuthenticationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                User? foundUser = await dbContext.Users.FirstOrDefaultAsync(user => user.Credentials.UserName.Equals(loginRequest.UserName, StringComparison.CurrentCultureIgnoreCase) &&
                user.Credentials.Password.Equals(loginRequest.Password, StringComparison.CurrentCultureIgnoreCase));

                if (foundUser is not null)
                {
                    foundUser.Credentials.UserName = loginRequest.UserName;
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
                if(authService.ValidateNewUserCredentials(request, dbContext.Credentials)) /* Returns true if credentials are unique */
                {
                    User user = authService.NewUserRequestToUserModel(request);
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    return CreatedAtRoute(request, authService.GetNewUserResponse(user));
                }
                return BadRequest("Given credentials not unique.");
            }
            return BadRequest("Password was not confirmed.");
        }
    }
}
