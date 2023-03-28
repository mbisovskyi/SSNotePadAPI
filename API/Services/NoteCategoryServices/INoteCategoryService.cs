using API.Models.NoteCategoryModels;
using API.Requests.NoteCategoryRequest;

namespace API.Services.NoteCategoryServices
{
    public interface INoteCategoryService
    {
        public NoteCategory CreateCategoryRequestToNoteCategoryModel(CreateCategoryRequest createCategoryRequest);
    }
}
