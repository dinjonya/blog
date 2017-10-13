using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class CategoryPosts_Model
    {
        public int PageCount { get; set; }
        public List<CategoryPost_Model> Posts { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    

    public class CategoryPost_Model
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDesc { get; set; }
        public string PostContent { get; set; }
        public string PostTime { get; set; }    
        public int PostCategoryId { get; set; }
        public string PostCategory { get; set; }
    }
}