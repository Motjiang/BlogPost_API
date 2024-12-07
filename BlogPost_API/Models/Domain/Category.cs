namespace BlogPost_API.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
