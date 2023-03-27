using API.Database;
using API.Models.NoteCategoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class NoteCategoryController : AppController
    {
        private readonly DatabaseDbContext dbContext;

        public NoteCategoryController(DatabaseDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNoteCategory_Async(NoteCategory request) 
        {
            bool isUserId = await dbContext.Users.FindAsync(request.UserId) != null;
            if (isUserId)
            {
                NoteCategory? foundCategory = await dbContext.NoteCategories.FirstOrDefaultAsync(category => category.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) &&
                category.UserId.Equals(request.UserId));

                if (foundCategory == null) 
                {
                    await dbContext.NoteCategories.AddAsync(request);
                    await dbContext.SaveChangesAsync();
                    return Ok($"Category \"{request.Name}\" for user is created");
                }
                return BadRequest($"Category \"{request.Name}\" for this user already exists.");
            }
            return NotFound("Not valid user.");
         }
    }
}
