using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Entities
{
    [Collection(nameof(DomainFixtureCollection))]
    [Trait(TestCategory, TestType)]
    public class BreedDomainTests
    {
        private readonly DomainFixture _fixture;
        public const string TestType = "Domain";
        public const string TestCategory = "Breed Domain Tests";

        public BreedDomainTests(DomainFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory(DisplayName = @"GIVEN null or empty or white spaced Name 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Case_01(string name)
        {
            //arrange
            //act
            var result = () => new Breed(name, BreedType.Terrier.Id, "Toy", "England");

            //Assert
            result.Should().Throw<ArgumentException>();
        }

        [Theory(DisplayName = @"GIVEN invalid length name less than 3 or greater than 150 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData("Yo")]
        [InlineData("YorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshire")]
        public void Case_02(string name)
        {
            //arrange
            //act
            var result = () => new Breed(name, BreedType.Terrier.Id, "Toy", "England");

            //Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory(DisplayName = @"GIVEN null or empty or white spaced Family 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Case_03(string family)
        {
            //arrange
            //act
            var result = () => new Breed("Yorkshire", BreedType.Terrier.Id, family, "England");

            //Assert
            result.Should().Throw<ArgumentException>();
        }

        [Theory(DisplayName = @"GIVEN invalid length Family less than 3 or greater than 150 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData("Yo")]
        [InlineData("YorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshire")]
        public void Case_04(string family)
        {
            //arrange
            //act
            var result = () => new Breed("Yorkshire", BreedType.Terrier.Id, family, "England");

            //Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory(DisplayName = @"GIVEN null or empty or white spaced Origin 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Case_05(string origin)
        {
            //arrange
            //act
            var result = () => new Breed("Yorkshire", BreedType.Terrier.Id, "Toy", origin);

            //Assert
            result.Should().Throw<ArgumentException>();
        }

        [Theory(DisplayName = @"GIVEN invalid length Origin less than 3 or greater than 150 
WHEN new Breed is instanciated
SHOULD throw ArgumentException")]
        [InlineData("Yo")]
        [InlineData("YorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshireYorkshire")]
        public void Case_06(string origin)
        {
            //arrange
            //act
            var result = () => new Breed("Yorkshire", BreedType.Terrier.Id, "Toy", origin);

            //Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact(DisplayName = @"GIVEN invalid BreedTypeId 
WHEN new Breed is instanciated
SHOULD throw EnumerationKeyOutOfRangeDomainException")]
        public void Case_08()
        {
            //arrange
            //act
            var result = () => new Breed("Yorkshire", 99, "Toy", "England");

            //Assert
            result.Should().Throw<EnumerationKeyOutOfRangeDomainException>();
        }

        [Fact(DisplayName = @"GIVEN valid Breed properties
WHEN new Breed is instanciated
SHOULD should create a valid Breed Entity")]
        public void Case_09()
        {
            //arrange
            //act
            var result = new Breed("Yorkshire", BreedType.Terrier.Id, "Toy", "England");

            //Assert
            result.Should().BeOfType<Breed>();
            result.Name.Should().Be("Yorkshire");
            result.BreedType.Id.Should().Be(BreedType.Terrier.Id);
            result.Family.Should().Be("Toy");
            result.Origin.Should().Be("England");
            result.ExternalId.Should().NotBeEmpty();
            result.IsTransient.Should().BeTrue();
        }
    }
}
