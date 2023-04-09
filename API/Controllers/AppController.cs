using API.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = AuthScheme)]
    public class AppController : ControllerBase
    {
        private const string AuthScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}
