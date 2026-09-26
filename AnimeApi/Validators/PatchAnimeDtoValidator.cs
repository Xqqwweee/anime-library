using FluentValidation;
using AnimeApi.DTOs;

namespace AnimeApi.Validators
{
    public class PatchAnimeDtoValidator : AbstractValidator<PatchAnimeDto>
    {
        public PatchAnimeDtoValidator()
        {

            RuleFor(dto => dto.Title).NotEmpty().When(dto => dto.Title is not null);
            RuleFor(dto => dto.Episodes).GreaterThan(0).When(dto => dto.Episodes is not null);
            RuleFor(dto => dto.Rating).GreaterThan(0).When(dto => dto.Rating is not null);
            RuleFor(dto => dto.Rating).LessThanOrEqualTo(10).When(dto => dto.Rating is not null);
        }
    }
}
