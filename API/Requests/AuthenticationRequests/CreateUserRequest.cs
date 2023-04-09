namespace API.Requests.AuthenticationRequests
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPasswordConfirmed { get; set; }
        public string Email { get; set; }
    }
}
