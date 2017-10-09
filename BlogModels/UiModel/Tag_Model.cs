using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class Tag_Model
    {
        public List<TagModel> Tags { get; set; }
    }
    public class TagModel
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public int PostNum { get; set; }
    }
}