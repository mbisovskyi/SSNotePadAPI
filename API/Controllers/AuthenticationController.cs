using API.Database;
using API.Models.UserModels;
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
        private readonly IAuthenticationService userService;

        public AuthenticationController(DatabaseDbContext context, IAuthenticationService service)
        {
            dbContext = context;
            userService = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserCredentials loginRequest)
        {
        UserCredentials? foundCredentials = await dbContext.Credentials.FirstOrDefaultAsync(credentialsRow =>
            credentialsRow.UserName.Equals(loginRequest.UserName, StringComparison.CurrentCultureIgnoreCase) &&
            credentialsRow.Password.Equals(loginRequest.Password, StringComparison.CurrentCultureIgnoreCase)) ;

        if (foundCredentials is not null) 
            {
                User foundUser = await dbContext.Users.SingleAsync(user => user.Id.Equals(foundCredentials.UserId));
                return Ok(userService.GetLoginUserResponse(foundUser, foundCredentials));
            }

        return BadRequest("Wrong user credentials.");
        }

        [HttpPost("NewUser")]
        public async Task<IActionResult> CreateNewUser_Async(User newUserRequest)
        {
            bool isNewUserRequestValid = userService.ValidateNewUserCredentials(newUserRequest, dbContext.Credentials);
            if (isNewUserRequestValid)
            {
                await dbContext.AddAsync(newUserRequest);
                await dbContext.SaveChangesAsync();
                return Ok("Successfully created.");
            }
            return BadRequest("Not valid credentials.");
        }
    }
}
