using API.Database;
using API.Models.NoteCategoryModels;
using API.Requests.NoteCategoryRequest;
using API.Services.NoteCategoryServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class NoteCategoryController : AppController
    {
        private readonly DatabaseDbContext dbContext;
        private readonly INoteCategoryService noteCategoryService;

        public NoteCategoryController(DatabaseDbContext _dbContext, INoteCategoryService _noteCategoryService)
        {
            dbContext = _dbContext;
            noteCategoryService = _noteCategoryService;
        }

        [HttpGet("UserCategories")]
        public async Task<IActionResult> GetUserNoteCategories(UserNoteCategoriesRequest request) 
        {
            List<NoteCategory> userCategories = await dbContext.NoteCategories.Where(category => category.UserId.Equals(request.UserId)).ToListAsync();
            return Ok(userCategories);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNoteCategory_Async(CreateCategoryRequest createCategoryRequest) 
        {
            List<NoteCategory> userCategories = await dbContext.NoteCategories.Where(category => category.UserId.Equals(createCategoryRequest.UserId) && 
                category.Name.Equals(createCategoryRequest.Name, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();

            if(userCategories.Count <= 0) 
            {
                NoteCategory newCategory = noteCategoryService.CreateCategoryRequestToNoteCategoryModel(createCategoryRequest);
                await dbContext.NoteCategories.AddAsync(newCategory);
                await dbContext.SaveChangesAsync();
                return Ok($"Created {newCategory.Name}");
            }
            return BadRequest("Category already exist.");
         }
    }
}
