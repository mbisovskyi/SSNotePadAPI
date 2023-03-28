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
            List<NoteCategory> userCategories = new List<NoteCategory>();
            userCategories = await dbContext.NoteCategories.Where(category => category.UserId.Equals(request.UserId)).ToListAsync();
            return Ok(userCategories);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateNoteCategory_Async(CreateCategoryRequest createCategoryRequest) 
        {
            bool isUserId = await dbContext.Users.SingleAsync(user => user.Id.Equals(createCategoryRequest.UserId)) != null;
            if (isUserId)
            {
                NoteCategory? foundCategory = await dbContext.NoteCategories.FirstOrDefaultAsync(category => category.Name.Equals(createCategoryRequest.Name, StringComparison.CurrentCultureIgnoreCase) &&
                category.UserId.Equals(createCategoryRequest.UserId));

                if (foundCategory == null) 
                {
                    await dbContext.NoteCategories.AddAsync(noteCategoryService.CreateCategoryRequestToNoteCategoryModel(createCategoryRequest));
                    await dbContext.SaveChangesAsync();

                    NoteCategory createdCategory = await dbContext.NoteCategories.SingleAsync(category => category.UserId.Equals(createCategoryRequest.UserId));
                    if (createdCategory != null) 
                    {
                        return Ok($"Category \"{createCategoryRequest.Name}\" was added.");
                    }
                    return BadRequest("Category was not saved.");
                }
                return BadRequest($"Category \"{createCategoryRequest.Name}\" for this user already exists.");
            }
            return NotFound("Not valid user.");
         }
    }
}
