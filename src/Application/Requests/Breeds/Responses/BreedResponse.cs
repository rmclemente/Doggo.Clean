using Application.SeedWork;

namespace Application.Requests.Breeds.Responses
{
    public class BreedResponse : AuditableResponse
    {
        public string Name { get; set; }
        public EnumerationResponse Type { get; set; }
        public string Family { get; set; }
        public string Origin { get; set; }

        public BreedResponse(Guid externalId, string name, EnumerationResponse type, string family, string origin, DateTime? createdAt, string createdBy, DateTime? lastUpdateAt, string lastUpdateBy)
            : base(externalId, createdAt, createdBy, lastUpdateAt, lastUpdateBy)
        {
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
        }
    }
}
