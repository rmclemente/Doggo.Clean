using AutoMapper;
using Doggo.Application.AutoMapper;
using Xunit;

namespace Doggo.Application.Tests.Mapper
{
    public class MappingTests
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public MappingTests()
        {
            _mapperConfiguration = new MapperConfiguration(c => c.AddProfile<Mappings>());
        }

        [Fact(DisplayName = "Mapping Configuration Should be Valid")]
        [Trait("Categoria", "Mapping Tests")]
        public void Mapping_Configuration_ShouldBeValid()
        {
            //https://docs.automapper.org/en/stable/Configuration-validation.html
            //arrange
            //Act
            //Assert
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
