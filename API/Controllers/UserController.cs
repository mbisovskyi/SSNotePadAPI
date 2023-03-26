using API.Database;
using API.Models.UserModels;
using API.Response.UserResponses;
using API.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseDbContext dbContext;
        private readonly IUserService userService;
        public UserController(DatabaseDbContext context, IUserService service)
        {
            dbContext = context;
            userService = service;
        }

        [HttpPost("/Login")]
        public IActionResult LoginUser(UserCredentials? request)
        {
            if(request?.GetType() == typeof(UserCredentials)) 
            {
                User? user = dbContext.Users.FirstOrDefault(user => user.Credentials.UserName.Equals(request.UserName, StringComparison.CurrentCultureIgnoreCase) && 
                user.Credentials.Password.Equals(request.Password, StringComparison.CurrentCultureIgnoreCase));
                UserCredentials? userCredentials = dbContext.Credentials.Find(user?.Id);

                if (user is not null && userCredentials is not null)
                {
                    return Ok(userService.GetLoginUserResponse(user, userCredentials));
                }
                return Forbid();   
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/NewUser")]
        public async Task<IActionResult> CreateNewUser_Async(User? request)
        {
            if (request is not null)
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
