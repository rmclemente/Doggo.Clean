using System;

namespace Doggo.Domain.Objects
{
    public abstract class AuditableEntity<T> : Entity<T> where T : class
    {
        public DateTime? CreatedAt { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public string UpdatedBy { get; protected set; }

        protected override void AddNotComparableProperties()
        {
            NotComparableProperties.Add(GetType().GetProperty(nameof(Id)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(UniqueId)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(ValidationResult)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(CreatedAt)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(CreatedBy)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(UpdatedAt)));
            NotComparableProperties.Add(GetType().GetProperty(nameof(UpdatedBy)));
        }
    }
}
