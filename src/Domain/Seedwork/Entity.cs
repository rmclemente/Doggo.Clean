using Domain.Core.SeedWork;
using System.Reflection;

namespace Domain.Seedwork
{
    public abstract class Entity
    {
        public int Id { get; private set; }
        public Guid ExternalId { get; protected set; }
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();
        private List<Event> _domainEvents;
        public IReadOnlyDictionary<string, object> Updates => _updates;
        private Dictionary<string, object> _updates;
        public bool IsTransient => Id == default;

        protected Entity()
        {
            ExternalId = Guid.NewGuid();
        }

        protected Entity(Guid externalId)
        {
            ExternalId = externalId;
        }

        public abstract void Validate();

        /// <summary>
        /// Get all properties used to be internally compared or copied by Entity.Equals and Entity.CopyDataFrom.
        /// </summary>
        /// <returns>Collection of PropertyInfo</returns>
        public abstract IEnumerable<string> GetEqualityProperties();

        /// <summary>
        /// Compare all properties listed by GetEqualityProperties with another class of the same type.
        /// Do not override default Equals.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If Equality Properties are Equals</returns>
        public virtual bool EqualityPropertiesEqualsTo(Entity other)
        {
            return GetType() == other.GetType() ?
                GetEqualityPropertiesHashCode() == other.GetEqualityPropertiesHashCode()
                : throw new ArgumentException($"Source type {GetType().Name} do not match target type {other.GetType().Name}");
        }

        /// <summary>
        /// Copy value of all properties listed by GetEqualityProperties, if not equals, from another class of the same type.
        /// </summary>
        /// <param name="source">Type T</param>
        /// <returns>True: If at least one value were copied</returns>
        public virtual bool CopyDataFrom(Entity source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source), "Source is null");
            if (EqualityPropertiesEqualsTo(source)) return false;

            foreach (string property in GetEqualityProperties())
            {
                SetSourceValueToTargetProperty(source, property);
                SetSourceValueToTargetField(source, property);
            }

            return EqualityPropertiesEqualsTo(source);
        }

        private void SetSourceValueToTargetProperty(Entity source, string property)
        {
            var targetProp = GetType().GetProperty(property);
            if (targetProp is null) return;

            var targetValue = targetProp.GetValue(this);
            var sourceValue = source.GetType().GetProperty(property).GetValue(source);

            if (Equals(targetValue, sourceValue) || !targetProp.CanWrite) return;

            targetProp.SetValue(this, sourceValue);
            AddUpdates(targetProp.Name, sourceValue);
        }

        private void SetSourceValueToTargetField(Entity source, string property)
        {
            var targetField = GetType().GetField(property, BindingFlags.NonPublic | BindingFlags.Instance);
            if (targetField is null) return;

            var targetValue = targetField.GetValue(this);
            var sourceValue = source.GetType().GetField(property, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(source);

            if (Equals(targetValue, sourceValue)) return;

            targetField.SetValue(this, sourceValue);
            AddUpdates(targetField.Name, sourceValue);
        }

        /// <summary>
        /// Generate hash based on all properties listed by GetEqualityProperties.
        /// </summary>
        /// <returns>int</returns>
        private int GetEqualityPropertiesHashCode()
        {
            var hash = new HashCode();
            PropertyInfo currentProperty;
            FieldInfo currentField;
            foreach (string property in GetEqualityProperties())
            {
                currentProperty = GetType().GetProperty(property);
                currentField = GetType().GetField(property, BindingFlags.NonPublic | BindingFlags.Instance);

                if (currentProperty is not null)
                    hash.Add(currentProperty.GetValue(this));

                if (currentField is not null)
                    hash.Add(currentField.GetValue(this));
            }
            return hash.ToHashCode();
        }

        public void AddUpdates(string field, object value)
        {
            _updates ??= new Dictionary<string, object>();
            _updates.Add(field, value);
        }

        public void ClearUpdates()
        {
            _updates?.Clear();
        }

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}