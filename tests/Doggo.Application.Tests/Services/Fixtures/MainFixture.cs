using AutoMapper;
using Doggo.Application.AutoMapper;
using Moq.AutoMock;
using System;
using Xunit;

namespace Doggo.Application.Tests.Services.Fixtures
{
    [CollectionDefinition(nameof(ApplicationServiceFixtureCollection))]
    public partial class ApplicationServiceFixtureCollection : ICollectionFixture<ApplicationServiceFixture>
    {

    }
    public partial class ApplicationServiceFixture : IDisposable
    {
        public AutoMocker Mocker;

        public IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<Mappings>());
            return mapperConfig.CreateMapper();
        }

        public void Dispose()
        {
        }
    }
}
