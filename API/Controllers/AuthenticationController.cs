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
                UserCredentials? foundCredentials = await dbContext.Credentials.FirstOrDefaultAsync(credentialsRow =>
                 credentialsRow.UserName.Equals(loginRequest.UserName, StringComparison.CurrentCultureIgnoreCase) &&
                 credentialsRow.Password.Equals(loginRequest.Password, StringComparison.CurrentCultureIgnoreCase));

                if (foundCredentials is not null)
                {
                    User foundUser = await dbContext.Users.SingleAsync(user => user.Id.Equals(foundCredentials.UserId));
                    return Ok(authService.GetLoginUserResponse(foundUser, foundCredentials));
                }

                return BadRequest("Wrong user credentials.");
            }

            return BadRequest("Password is not confirmed.");
        }

        [HttpPost("NewUser")]
        public async Task<IActionResult> CreateNewUser_Async(CreateUserRequest request)
        {
            if (request.IsPasswordConfirmed)
            {
               User user = authService.NewUserRequestToUserModel(request);
                if(authService.ValidateNewUserCredentials(user, dbContext.Credentials)) 
                {
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();

                    User? createdUser = await dbContext.Users.FirstOrDefaultAsync(user => user.Credentials.UserName.Equals(request.UserName, StringComparison.CurrentCultureIgnoreCase) &&
                    user.Credentials.Email.Equals(request.Email, StringComparison.CurrentCultureIgnoreCase));

                    if (createdUser != null) 
                    {
                        return CreatedAtRoute(request, authService.GetNewUserResponse(createdUser));
                    }
                    return BadRequest("Credentials were not saved.");
                }
                return BadRequest("Given credentials not unique.");
            }

            return BadRequest("Password was not confirmed.");
        }
    }
}
