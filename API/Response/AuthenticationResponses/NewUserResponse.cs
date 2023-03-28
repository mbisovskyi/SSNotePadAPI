namespace API.Response.AuthenticationResponses
{
    public class NewUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsOwnerOperator { get; set; }
    }
}
