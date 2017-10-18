using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class TagPosts_Model
    {
        public int PageCount { get; set; }
        public List<TagPost_Model> Posts { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }

    public class TagPost_Model
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDesc { get; set; }
        public string PostContent { get; set; }
        public long PostTime { get; set; }    
        public int PostCategoryId { get; set; }
        public string PostCategory { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}