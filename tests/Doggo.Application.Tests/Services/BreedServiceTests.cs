using AutoMapper;
using Doggo.Application.Dtos;
using Doggo.Application.Tests.Services.Fixtures;
using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication;
using Doggo.Infra.CrossCutting.Messages.Notifications;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Doggo.Application.Tests.Services.Parametrizacao
{
    [Collection(nameof(ApplicationServiceFixtureCollection))]
    public class BreedServiceTests
    {
        private readonly ApplicationServiceFixture _fixture;

        public BreedServiceTests(ApplicationServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Mapping From Valid Dto Create Valid Breed")]
        [Trait("Categoria", "Breed Service Tests")]
        public void BreedMapping_MapFromDtoToBreed_ShouldCreateValidMap()
        {
            //arrange
            var dto = _fixture.GenerateValidBreedDto();

            var mapper = _fixture.GetMapper();

            //Act
            var breed = mapper.Map<Breed>(dto);

            //Assert
            Assert.True(breed.IsValid());
            Assert.True(breed.Id == dto.Id);
            Assert.True(breed.UniqueId == dto.UniqueId);
            Assert.True(breed.Name == dto.Name);
            Assert.True(breed.Type == dto.Type);
            Assert.True(breed.Family == dto.Family);
            Assert.True(breed.Origin == dto.Origin);
            Assert.True(breed.OtherNames == dto.OtherNames);
        }

        [Fact(DisplayName = "Mapping From Valid Breed Create Valid Dto")]
        [Trait("Categoria", "Breed Service Tests")]
        public void BreedMapping_MapFromBreedToDto_ShouldCreateValidMap()
        {
            //arrange
            var breed = _fixture.GenerateValidBreed();
            var mapper = _fixture.GetMapper();

            //Act
            var dto = mapper.Map<BreedDto>(breed);

            //Assert
            Assert.True(breed.IsValid());
            Assert.True(breed.Id == dto.Id);
            Assert.True(breed.UniqueId == dto.UniqueId);
            Assert.True(breed.Name == dto.Name);
            Assert.True(breed.Type == dto.Type);
            Assert.True(breed.Family == dto.Family);
            Assert.True(breed.Origin == dto.Origin);
            Assert.True(breed.OtherNames == dto.OtherNames);
        }

        [Fact(DisplayName = "Should Call Add Breed From Repository When Valid")]
        [Trait("Categoria", "Breed Service Tests")]
        public async Task BreedAdd_ValidBreed_ShouldCallBreedRepository()
        {
            //arrange
            var service = _fixture.GetBreedService();
            var dto = _fixture.GenerateValidBreedDto();
            var mapper = _fixture.GetMapper();
            var Breed = mapper.Map<Breed>(dto);

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            _fixture.Mocker.GetMock<IMapper>()
                .Setup(r => r.Map<BreedDto>(It.IsAny<Breed>()))
                .Returns(mapper.Map<BreedDto>(Breed));

            //act
            var result = await service.Add(dto);

            //assert
            Assert.True(Breed.IsValid());
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.Add(It.IsAny<Breed>()), Times.Once);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Once);
            _fixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.PublishNotification(It.IsAny<DomainNotification>()), Times.Never);
        }

        [Fact(DisplayName = "Should Not Call Add Breed From Repository When Invalid")]
        [Trait("Categoria", "Breed Service Tests")]
        public async Task BreedAdd_InvalidBreed_ShouldNotCallBreedRepository()
        {
            //arrange
            var service = _fixture.GetBreedService();

            var dto = _fixture.GenerateInvalidBreedDto();
            var mapper = _fixture.GetMapper();
            var Breed = mapper.Map<Breed>(dto);

            //act
            var result = await service.Add(dto);

            //assert
            Assert.False(Breed.IsValid());
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.Add(It.IsAny<Breed>()), Times.Never);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Never);
            _fixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.PublishNotification(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Should Call Update Breed From Repository When Valid")]
        [Trait("Categoria", "Breed Service Tests")]
        public async Task BreedUpdate_ValidBreed_ShouldCallBreedRepository()
        {
            //arrange
            var service = _fixture.GetBreedService();
            var dto = _fixture.GenerateValidBreedDto();
            var current = _fixture.GenerateValidBreed();

            var mapper = _fixture.GetMapper();
            var mapped = mapper.Map<Breed>(dto);

            _fixture.Mocker.GetMock<IMapper>()
                .Setup(r => r.Map<Breed>(It.IsAny<BreedDto>()))
                .Returns(mapped);

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.Get(It.IsAny<Guid>(), true))
                .Returns(Task.FromResult(current));

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            //act
            var result = await service.Update(dto.UniqueId, dto);

            //assert
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.Update(It.IsAny<Breed>()), Times.Once);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Once);
            _fixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.PublishNotification(It.IsAny<DomainNotification>()), Times.Never);
        }

        [Fact(DisplayName = "Should Not Call Update Breed From Repository When Invalid")]
        [Trait("Categoria", "Breed Service Tests")]
        public async Task BreedUpdate_InvalidBreed_ShouldNotCallBreedRepository()
        {
            //arrange
            var service = _fixture.GetBreedService();

            var dto = _fixture.GenerateInvalidBreedDto();
            var current = _fixture.GenerateValidBreed();

            var mapper = _fixture.GetMapper();
            var mapped = mapper.Map<Breed>(dto);

            _fixture.Mocker.GetMock<IMapper>()
                .Setup(r => r.Map<Breed>(It.IsAny<BreedDto>()))
                .Returns(mapped);

            _fixture.Mocker.GetMock<IBreedRepository>()
                .Setup(r => r.Get(It.IsAny<Guid>(), true))
                .Returns(Task.FromResult(current));

            //act
            var result = await service.Update(dto.UniqueId, dto);

            //assert
            Assert.False(mapped.IsValid());
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.Update(It.IsAny<Breed>()), Times.Never);
            _fixture.Mocker.GetMock<IBreedRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Never);
            _fixture.Mocker.GetMock<IMediatorHandler>().Verify(p => p.PublishNotification(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }
    }
}
