using System;
using Xunit;

namespace Doggo.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(DomainFixtureCollection))]
    public partial class DomainFixtureCollection : ICollectionFixture<DomainFixture>
    {

    }
    public partial class DomainFixture : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
