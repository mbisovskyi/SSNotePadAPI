using API.Models.NoteModels;
using API.Requests.NoteRequests;

namespace API.Services.NoteServices
{
    public class NoteService : INoteService
    {
        public Note FromNewNoteRequestToNoteModel(CreateNoteRequest newNoteRequest)
        {
            Note note = new Note()
            {
                UserId = newNoteRequest.UserId,
                NoteCategoryId = newNoteRequest.NoteCategoryId,
                Title = newNoteRequest.Title,
                Description = newNoteRequest.Description,
             };
            return note;
        }
     }
}
