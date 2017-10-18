using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class DatePosts_Model
    {
        public int Year { get; set; }
        public int Month { get; set; } 
        public List<DatePost_Model> Posts { get; set; }
    }
    public class DatePost_Model
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDesc { get; set; }
        public long PostTime { get; set; }    
        public int PostCategoryId { get; set; }
        public string PostCategory { get; set; }
    }
}