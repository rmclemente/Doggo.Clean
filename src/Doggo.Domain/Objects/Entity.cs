using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Doggo.Domain.Objects
{
    public abstract class Entity<T> : IEquatable<T> where T : class
    {
        public int Id { get; protected set; }
        public Guid UniqueId { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected ICollection<PropertyInfo> NotComparableProperties;

        protected Entity()
        {
            UniqueId = Guid.NewGuid();
            NotComparableProperties = new HashSet<PropertyInfo>();
            AddNotComparableProperties();
        }

        public abstract bool IsValid();

        protected virtual void AddNotComparableProperties()
        {
            NotComparableProperties.Add(GetType().GetProperty(nameof(Id)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(UniqueId)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(ValidationResult)));
        }

        /// <summary>
        /// Compare all properties not listed as NotComparableProperties with another class of the same type.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If any value were copied</returns>
        public virtual bool Equals([AllowNull] T other)
        {
            if (other == null) return false;

            var comparableProperties = GetType().GetProperties().Except(NotComparableProperties);
            object thisValue;
            object otherValue;

            foreach (var property in comparableProperties)
            {
                thisValue = property.GetValue(this, null);
                otherValue = other.GetType().GetProperty(property.Name).GetValue(other, null);

                if (!Equals(thisValue, otherValue))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Copy value of all properties not listed at NotComparableProperties, if not equals, from another class of the same type.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If any value were copied</returns>
        public virtual bool CopyDataFrom(T source)
        {
            if (source == null) return false;
            if (Equals(source)) return false;

            var targetProperties = GetType().GetProperties().Except(NotComparableProperties);
            object targetValue;
            object sourceValue;

            foreach (var property in targetProperties)
            {
                targetValue = property.GetValue(this, null);
                sourceValue = source.GetType().GetProperty(property.Name).GetValue(source, null);

                if (!Equals(targetValue, sourceValue) && property.CanWrite)
                    property.SetValue(this, sourceValue);
            }

            return Equals(source);
        }

        public override int GetHashCode()
        {
            var comparableProperties = GetType().GetProperties().Except(NotComparableProperties);
            var hash = new HashCode();

            foreach (var property in comparableProperties)
                hash.Add(property.GetValue(this, null));

            return hash.ToHashCode();
        }
    }
}
