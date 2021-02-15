using System;

namespace Doggo.Application.Dtos
{
    public class BreedDto : BaseDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Family { get; set; }
        public string Origin { get; set; }
        public DateTime? DateOfOrigin { get; set; }
        public string OtherNames { get; set; }
    }
}
