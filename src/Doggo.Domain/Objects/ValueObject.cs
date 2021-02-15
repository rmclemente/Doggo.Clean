using FluentValidation.Results;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Doggo.Domain.Objects
{
    public abstract class ValueObject<T> : IEquatable<T> where T : class
    {
        public ValidationResult ValidationResult { get; protected set; }

        protected ValueObject() { }

        public abstract bool IsValid();

        public virtual bool Equals([AllowNull] T other)
        {
            var properties = GetType().GetProperties().Where(p => !p.Name.Equals(nameof(ValidationResult)));

            object thisValue;
            object otherValue;

            foreach (var item in properties)
            {
                thisValue = item.GetValue(this, null);
                otherValue = other.GetType().GetProperty(item.Name).GetValue(other, null);

                if (!thisValue.Equals(otherValue))
                    return false;
            }

            return true;
        }
    }
}
