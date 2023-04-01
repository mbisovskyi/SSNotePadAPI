using API.Database;
using API.Models.NoteCategoryModels;
using API.Models.NoteModels;
using API.Requests.NoteRequests;
using API.Services.NoteServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class NoteController : AppController
    {
        private readonly DatabaseDbContext dbContext;
        private readonly INoteService noteService;
        public NoteController(DatabaseDbContext _context, INoteService _service) 
        {
            dbContext = _context;
            noteService = _service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewNote_Async(CreateNoteRequest newNoteRequest)
        {
            List <NoteCategory> userCategories = await dbContext.NoteCategories.Where(category => category.UserId.Equals(newNoteRequest.UserId)).ToListAsync();
            if (userCategories.Count <= 0) { 
                return BadRequest("Invalid User Credentials"); 
            }

            NoteCategory? noteCategory = new NoteCategory();
            if (newNoteRequest.NoteCategoryId > userCategories.Count)
            {
                noteCategory = userCategories.Find(category => category.Name.Equals("General"));
                newNoteRequest.NoteCategoryId = noteCategory.Id;
            }
            else
            {
                noteCategory = userCategories.Find(category => category.Id.Equals(newNoteRequest.NoteCategoryId));
            }

            Note newNote = noteService.FromNewNoteRequestToNoteModel(newNoteRequest);
            noteCategory.NotesQuantity++;

            await dbContext.AddAsync(newNote);
            await dbContext.SaveChangesAsync();

            return Ok(newNote);
        }
    }
};
    