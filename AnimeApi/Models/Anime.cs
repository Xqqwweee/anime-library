namespace AnimeApi.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Episodes { get; set; }
        public double Rating { get; set; }
    }
}