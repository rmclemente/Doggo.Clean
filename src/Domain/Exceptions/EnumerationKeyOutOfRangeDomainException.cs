using Domain.Core.SeedWork;

namespace Domain.Exceptions
{
    public class EnumerationKeyOutOfRangeDomainException : DomainException
    {
        public EnumerationKeyOutOfRangeDomainException(string entity, string property, string value, string validArguments) : base($"{entity} {property} '{value}' is out of range. Valid values '{validArguments}'") { }

        public EnumerationKeyOutOfRangeDomainException(string entity, string property, string value) : base($"{entity} {property} '{value}' is out of range.") { }
    }
}
