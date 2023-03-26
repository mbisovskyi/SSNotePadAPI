using API.Database;
using API.Models.UserModels;
using API.Response.UserResponses;
using API.Services.AuthenticationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [HttpPost("/Login")]
        public IActionResult LoginUser(UserCredentials? request)
        {
            if(request?.GetType() == typeof(UserCredentials)) 
            {
                UserCredentials? foundCredentials = dbContext.Credentials.FirstOrDefault(credsRow =>
                    credsRow.UserName.Equals(request.UserName, StringComparison.CurrentCultureIgnoreCase) &&
                    credsRow.Password.Equals(request.Password, StringComparison.CurrentCultureIgnoreCase)
                ) ;

                User? user = dbContext.Users.Find(foundCredentials?.UserId);
                if (user == null) return BadRequest();
                return Ok(userService.GetLoginUserResponse(user, foundCredentials));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/NewUser")]
        public async Task<IActionResult> CreateNewUser_Async(User? request)
        {
            if (request?.GetType() == typeof(User))
            {
                User? foundUser = dbContext.Users.FirstOrDefault(user => user.Credentials.UserName.Equals(request.Credentials.UserName, StringComparison.CurrentCultureIgnoreCase) || 
                user.Credentials.Email.Equals(request.Credentials.Email, StringComparison.CurrentCultureIgnoreCase));
                if (foundUser is null)
                {
                    dbContext.Add(request);
                    await dbContext.SaveChangesAsync();
                    return Ok(request);
                }
                return BadRequest();

            }
            return BadRequest();
        }
    }
}
