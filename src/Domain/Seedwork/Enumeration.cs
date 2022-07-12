using System.Reflection;
using System.Runtime.CompilerServices;

namespace Domain.Seedwork
{
    public abstract class Enumeration<TEnum> : Enumeration<TEnum, int> where TEnum : Enumeration<TEnum, int>
    {
        protected Enumeration(int id, string name) : base(id, name)
        {
        }
    }

    public abstract class Enumeration<TEnum, TKey> :
        IEquatable<Enumeration<TEnum, TKey>>,
        IComparable<Enumeration<TEnum, TKey>>
        where TEnum : Enumeration<TEnum, TKey>
        where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        public TKey Id { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(TKey id, string name)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
        }

        static readonly Lazy<TEnum[]> _enumOptions =
            new(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

        static readonly Lazy<Dictionary<string, TEnum>> _fromName =
            new(() => _enumOptions.Value.ToDictionary(item => item.Name));

        static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase =
            new(() => _enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

        static readonly Lazy<Dictionary<TKey, TEnum>> _fromId =
            new(() =>
            {
                // multiple enums with same value are allowed but store only one per value
                var dictionary = new Dictionary<TKey, TEnum>();

                foreach (var item in _enumOptions.Value)
                    if (!dictionary.ContainsKey(item.Id))
                        dictionary.Add(item.Id, item);

                return dictionary;
            });

        private static TEnum[] GetAllOptions()
        {
            return typeof(TEnum).GetFields(BindingFlags.Public |
                                 BindingFlags.Static |
                                 BindingFlags.DeclaredOnly)
                      .Select(f => f.GetValue(null))
                      .Cast<TEnum>()
                      .OrderBy(t => t.Id)
                      .ToArray();
        }

        public static IReadOnlyCollection<TEnum> List =>
            _fromName.Value.Values.ToList().AsReadOnly();

        public Type GeTKeyType() => typeof(TKey);

        public static TEnum FromName(string name, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (ignoreCase)
                return FromName(_fromNameIgnoreCase.Value);

            return FromName(_fromName.Value);

            TEnum FromName(Dictionary<string, TEnum> dictionary)
            {
                if (!dictionary.TryGetValue(name, out var result))
                    throw new ArgumentException($"'{name}' is invalid value for {typeof(TEnum).Name} {nameof(Name)}. Possible values: {string.Join(", ", List.Select(p => $"'{p.Name}'"))}");

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFromName(string name, out TEnum result) =>
            TryFromName(name, false, out result);

        public static bool TryFromName(string name, bool ignoreCase, out TEnum result)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                result = default;
                return false;
            }

            if (ignoreCase)
                return _fromNameIgnoreCase.Value.TryGetValue(name, out result);

            return _fromName.Value.TryGetValue(name, out result);
        }

        public static TEnum FromId(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!_fromId.Value.TryGetValue(id, out var result))
                throw new ArgumentException($"{id} is invalid value for {typeof(TEnum).Name} {nameof(Id)}. Possible values: {string.Join(", ", List.Select(p => $"'{p.Id}'"))}");

            return result;
        }

        public static TEnum FromId(TKey id, TEnum defaulTKey)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (!_fromId.Value.TryGetValue(id, out var result))
                return defaulTKey;

            return result;
        }

        public static bool TryFromId(TKey id, out TEnum result)
        {
            if (id == null)
            {
                result = default;
                return false;
            }

            return _fromId.Value.TryGetValue(id, out result);
        }

        public static string ToJoinedValues(string separator = ", ") => string.Join(separator, List.Select(p => $"[{p.Id}] {p.Name}"));

        public override string ToString() => Name;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj) =>
           obj is Enumeration<TEnum, TKey> other && Equals(other);

        public bool Equals(Enumeration<TEnum, TKey> other)
        {
            // check if same instance
            if (ReferenceEquals(this, other))
                return true;

            // it's not same instance so 
            // check if it's not null and is same value
            if (other is null)
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right)
        {
            // Handle null on left side
            if (left is null)
                return right is null;

            // Equals handles null on right side
            return left.Equals(right);
        }

        public int CompareTo(Enumeration<TEnum, TKey> other) => Id.CompareTo(other.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right) => !(left == right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right) => left.CompareTo(right) < 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right) => left.CompareTo(right) <= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right) => left.CompareTo(right) > 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(Enumeration<TEnum, TKey> left, Enumeration<TEnum, TKey> right) => left.CompareTo(right) >= 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator TKey(Enumeration<TEnum, TKey> enumeration) => enumeration.Id;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Enumeration<TEnum, TKey>(TKey value) => FromId(value);
    }
}