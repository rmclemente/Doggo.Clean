using Application.SeedWork;

namespace Application.Requests.Breeds.Responses
{
    public class BreedBriefResponse : BaseResponse
    {
        public string Name { get; set; }
        public EnumerationResponse Type { get; set; }
        public string Family { get; set; }
        public string Origin { get; set; }

        public BreedBriefResponse(Guid externalId, string name, EnumerationResponse type, string family, string origin) : base(externalId)
        {
            Name = name;
            Type = type;
            Family = family;
            Origin = origin;
        }
    }
}
