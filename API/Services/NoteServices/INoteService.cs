using API.Models.NoteModels;
using API.Requests.NoteRequests;

namespace API.Services.NoteServices
{
    public interface INoteService
    {
        public Note FromNewNoteRequestToNoteModel(CreateNoteRequest newNoteRequest);
     }
}
