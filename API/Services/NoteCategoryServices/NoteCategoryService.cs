using API.Models.NoteCategoryModels;
using API.Requests.NoteCategoryRequest;

namespace API.Services.NoteCategoryServices
{
    public class NoteCategoryService : INoteCategoryService
    {
        public NoteCategory CreateCategoryRequestToNoteCategoryModel(CreateCategoryRequest createCategoryRequest)
        {
            NoteCategory noteCategory = new NoteCategory()
            {
                Name = createCategoryRequest.Name,
                UserId = createCategoryRequest.UserId,
            };

            return noteCategory;
        }
    }
}
