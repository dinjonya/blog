using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class Category_Model
    {
        public int PageCount { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int PostNum { get; set; }
    }
}