using Doggo.Domain.Objects;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Doggo.Domain.Entities
{
    public class Breed : AuditableEntity<Breed>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Family { get; private set; }
        public string Origin { get; private set; }
        public DateTime? DateOfOrigin { get; private set; }
        public string OtherNames { get; private set; }

        public Breed(string name, string type, string family, string origin, DateTime? dateOfOrigin, string otherNames)
        {
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
            DateOfOrigin = dateOfOrigin;
            OtherNames = otherNames;
        }

        protected Breed() { }

        public override bool IsValid()
        {
            ValidationResult = new BreedValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BreedValidator : AbstractValidator<Breed>
    {
        public BreedValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Type).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Family).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Origin).NotEmpty().MaximumLength(100);
            RuleFor(p => p.OtherNames).NotEmpty().MaximumLength(100);
        }
    }
}
