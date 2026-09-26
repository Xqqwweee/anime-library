using AnimeApi.Models;

namespace AnimeApi.DTOs
{
    public class AnimeResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Episodes { get; set; }
        public double Rating { get; set; }

        public static AnimeResponseDto ToDto(Anime anime)
        {
            AnimeResponseDto newDto = new();
            newDto.Id = anime.Id;
            newDto.Title = anime.Title;
            newDto.Episodes = anime.Episodes;
            newDto.Rating = anime.Rating;
            return newDto;
        }
    }
}