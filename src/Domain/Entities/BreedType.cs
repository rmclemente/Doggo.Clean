using Domain.Seedwork;

namespace Domain.Entities
{
    public sealed class BreedType : Enumeration<BreedType>
    {
        public static readonly BreedType Toy = new(1, nameof(Toy));
        public static readonly BreedType Terrier = new(2, nameof(Terrier));
        public static readonly BreedType Working = new(3, nameof(Working));
        private BreedType(int id, string name) : base(id, name) { }
    }
}
