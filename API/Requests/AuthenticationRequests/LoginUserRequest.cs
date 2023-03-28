using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace API.Requests.AuthenticationRequests
{
    public class LoginUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPasswordConfirmed { get; set; }
    }
}
