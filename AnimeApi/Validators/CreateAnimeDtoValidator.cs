using FluentValidation;
using AnimeApi.DTOs;

namespace AnimeApi.Validators
{
    public class CreateAnimeDtoValidator : AbstractValidator<CreateAnimeDto>
    {
        public CreateAnimeDtoValidator()
        {
            RuleFor(dto => dto.Title).NotEmpty();
            RuleFor(dto => dto.Episodes).GreaterThan(0);
            RuleFor(dto => dto.Rating).GreaterThan(0);
            RuleFor(dto => dto.Rating).LessThanOrEqualTo(10);
        }
    }
}
