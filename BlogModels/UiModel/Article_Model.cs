using System;
using System.Collections.Generic;

namespace BlogModels.UiModel
{
    public class Article_Model
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostPageKeywords { get; set; }
        public string PostDescription { get; set; }
        public string PostContent { get; set; }
        public int PostCategoryId { get; set; }
        public string PostCategoryName { get; set; }
        public List<TagModel> Tags { get; set; }
        public DateTime PostTime { get; set; }

        public NeighborPost PreviousPost { get; set; }

        public NeighborPost NextPost { get; set; }

    }

    public class NeighborPost
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
    }
}