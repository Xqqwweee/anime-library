namespace AnimeApi.DTOs
{
    public class CreateAnimeDto
    {
        public string Title { get; set; } = string.Empty;
        public int Episodes { get; set; }
        public double Rating { get; set; }
    }
}