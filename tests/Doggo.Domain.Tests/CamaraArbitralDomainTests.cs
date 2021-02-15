using Doggo.Domain.Entities;
using Doggo.Domain.Tests.Fixtures;
using FluentValidation.TestHelper;
using Xunit;

namespace Doggo.Domain.Tests.Parametrizacao
{
    [Collection(nameof(DomainFixtureCollection))]
    public class BreedDomainTests
    {
        private readonly DomainFixture _fixture;

        public BreedDomainTests(DomainFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "New Breed Should Be Valid")]
        [Trait("Categoria", "Breed Tests")]
        public void BreedValidation_NewBreed_ShouldBeValid()
        {
            //arrange
            var Breed = new Breed("Akita", "Working", "Northern", "Japan", null, "Akita Inu");

            //act
            var result = new BreedValidator().TestValidate(Breed);

            //assert
            Assert.True(Breed.IsValid());
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "New Breed Should Be Invalid")]
        [Trait("Categoria", "Breed Tests")]
        public void BreedValidation_NewBreed_ShouldBeInvalid()
        {
            //arrange
            var Breed = new Breed(null, null, null, null, null, null);

            //act
            var result = new BreedValidator().TestValidate(Breed);

            //Assert
            Assert.False(Breed.IsValid());
            Assert.False(result.IsValid);
            result.ShouldHaveValidationErrorFor(p => p.Name);
            result.ShouldHaveValidationErrorFor(p => p.Type);
            result.ShouldHaveValidationErrorFor(p => p.Family);
            result.ShouldHaveValidationErrorFor(p => p.Origin);
            result.ShouldHaveValidationErrorFor(p => p.OtherNames);
        }

        [Fact(DisplayName = "Atualizar propriedades de Breed baseado em outra Breed")]
        [Trait("Categoria", "Breed Tests")]
        public void Breed_UpdatePropertiesFromAnotherBreed_ActualAndOtherBreedSholdBeEquals()
        {
            //arrange
            var BreedAtual = _fixture.GenerateValidBreed();
            var BreedNovo = _fixture.GenerateValidBreed();

            //act - Assert
            Assert.True(BreedAtual.CopyDataFrom(BreedNovo));
            Assert.True(BreedAtual.Equals(BreedNovo));
        }
    }
}
