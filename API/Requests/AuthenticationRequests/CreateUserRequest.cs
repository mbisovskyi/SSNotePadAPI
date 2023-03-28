namespace API.Requests.AuthenticationRequests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPasswordConfirmed { get; set; }
        public string Email { get; set; }
        public bool IsOwnerOperator { get; set; }
    }
}
