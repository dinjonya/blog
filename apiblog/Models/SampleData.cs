namespace apiblog.Models
{
    public class SampleData
    {
        public static void Init(BlogEntities blogEntities)
        {
            blogEntities.SaveChanges();
        }
    }
}