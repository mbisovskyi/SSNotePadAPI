namespace API.Response.UserResponses
{
    public class LoginUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? Token { get; set; }
    }
}
