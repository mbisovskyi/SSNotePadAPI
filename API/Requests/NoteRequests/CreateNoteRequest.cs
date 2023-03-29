namespace API.Requests.NoteRequests
{
    public class CreateNoteRequest
    {
        public int UserId { get; set; }
        public int NoteCategoryId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
