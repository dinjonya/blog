using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class Index_Model
    {
        public int PageCount { get; set; }
        public List<IndexPost_Model> Posts { get; set; }
    }
    public class IndexPost_Model
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