
using FluentAssertions;

namespace Domain.Tests
{
    public class EntityTests
    {
        public const string TestType = "Domain";
        public const string TestCategory = "BaseEntity Tests";

        public EntityTests()
        {
        }

        [Fact(DisplayName = "GetEqualityProperties Should Return Entity Equality Properties")]
        [Trait(TestType, TestCategory)]
        public void GetEqualityProperties_ShouldReturnEntityEqualityProperties()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();

            //act
            var result = foo.GetEqualityProperties();

            //assert
            result.Any().Should().BeTrue();
        }

        [Fact(DisplayName = "EqualityPropertiesEqualsTo Should Be True When Equality Properties Are The Same")]
        [Trait(TestType, TestCategory)]
        public void EqualityPropertiesEqualsTo_ShouldBeTrue_WhenEqualityPropertiesAreTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var fooOther = new DomainFixture.Foo(foo.Name, foo.Age);

            //act
            var result = foo.EqualityPropertiesEqualsTo(fooOther);

            //assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "EqualityPropertiesEqualsTo Should Be False When Equality Properties Are Not The Same")]
        [Trait(TestType, TestCategory)]
        public void EqualityPropertiesEqualsTo_ShouldBeFalse_WhenEqualityPropertiesAreNotTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var fooOther = DomainFixture.GenerateFoo();

            //act
            var result = foo.EqualityPropertiesEqualsTo(fooOther);

            //assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "EqualityPropertiesEqualsTo Should Throw ArgumentException When Equality Properties Are Not The Same")]
        [Trait(TestType, TestCategory)]
        public void EqualityPropertiesEqualsTo_ShouldThrowArgumentException_WhenEntityTypesAreNotTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var bar = DomainFixture.GenerateBar();

            //act
            var result = () => foo.EqualityPropertiesEqualsTo(bar);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Fact(DisplayName = "CopyDataFrom Should Be True And Copy Values When Equality Properties Are Not The Same")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldBeTrue_And_CopyValues_WhenEqualityPropertiesAreNotTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var fooOther = DomainFixture.GenerateFoo();

            //act
            var result = foo.CopyDataFrom(fooOther);

            //assert
            result.Should().BeTrue();
            foo.Name.Should().BeEquivalentTo(fooOther.Name);
            foo.Age.Should().Be(fooOther.Age);
        }

        [Fact(DisplayName = "CopyDataFrom Should Be False When Equality Properties Are The Same")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldBeFalse_WhenEqualityPropertiesAreTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var fooOther = new DomainFixture.Foo(foo.Name, foo.Age);

            //act
            var result = foo.CopyDataFrom(fooOther);

            //assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "CopyDataFrom Should Throw ArgumentNullException When Source Is Null")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldThrowArgumentNullException_WhenSourceIsNull()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();

            //act
            var result = () => foo.CopyDataFrom(null);

            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "CopyDataFrom Should Throw ArgumentException When Equality Properties Are Not The Same")]
        [Trait(TestType, TestCategory)]
        public void CopyDataFrom_ShouldThrowArgumentException_WhenEntityTypesAreNotTheSame()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var bar = DomainFixture.GenerateBar();

            //act
            var result = () => foo.CopyDataFrom(bar);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Fact(DisplayName = "AddUpdates Should Add Key Value Pair To Updates List")]
        [Trait(TestType, TestCategory)]
        public void AddUpdates_ShouldAddKeyValuePairToUpdatesList()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();

            //act
            foo.AddUpdates(nameof(foo.Name), foo.Name);

            //assert
            foo.Updates.Any().Should().BeTrue();
            foo.Updates.Count.Should().Be(1);
        }

        [Fact(DisplayName = "ClearUpdates Should Clear Updates List")]
        [Trait(TestType, TestCategory)]
        public void ClearUpdates_ShouldClearUpdatesList()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            foo.AddUpdates(nameof(foo.Name), foo.Name);
            var countUpdates = foo.Updates.Count;

            //act
            foo.ClearUpdates();

            //assert
            foo.Updates.Any().Should().BeFalse();
            countUpdates.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "AddDomainEvent Should Add New Event To Domain Events List")]
        [Trait(TestType, TestCategory)]
        public void AddDomainEvent_ShouldAddNewEventToDomainEventsList()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();

            //act
            foo.AddDomainEvent(DomainFixture.GenerateFooDomainEvent());

            //assert
            foo.DomainEvents.Any().Should().BeTrue();
            foo.DomainEvents.Count.Should().Be(1);
        }

        [Fact(DisplayName = "RemoveDomainEvent Should Remove Event From Domain Events List")]
        [Trait(TestType, TestCategory)]
        public void RemoveDomainEvent_ShouldRemoveEventFromDomainEventsList()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var event1 = DomainFixture.GenerateFooDomainEvent();
            var event2 = DomainFixture.GenerateFooDomainEvent();
            foo.AddDomainEvent(event1);
            foo.AddDomainEvent(event2);
            var countBeforeRemove = foo.DomainEvents.Count;

            //act
            foo.RemoveDomainEvent(event2);

            //assert
            foo.DomainEvents.Any().Should().BeTrue();
            foo.DomainEvents.Count.Should().Be(1);
            countBeforeRemove.Should().BeGreaterThan(foo.DomainEvents.Count);
            foo.DomainEvents.Any(p => p.MessageId == event1.MessageId).Should().BeTrue();
        }

        [Fact(DisplayName = "ClearDomainEvents Should Clear Domain Events List")]
        [Trait(TestType, TestCategory)]
        public void ClearDomainEvents_ShouldClearDomainEventsList()
        {
            //arrange
            var foo = DomainFixture.GenerateFoo();
            var event1 = DomainFixture.GenerateFooDomainEvent();
            var event2 = DomainFixture.GenerateFooDomainEvent();
            foo.AddDomainEvent(event1);
            foo.AddDomainEvent(event2);
            var countBeforeRemove = foo.DomainEvents.Count;

            //act
            foo.ClearDomainEvents();

            //assert
            foo.DomainEvents.Any().Should().BeFalse();
            foo.DomainEvents.Count.Should().Be(0);
            countBeforeRemove.Should().BeGreaterThan(0);
        }
    }
}