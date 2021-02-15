using AutoMapper;
using Doggo.Application.Dtos;
using Doggo.Application.Interfaces;
using Doggo.Domain.Entities;
using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.CrossCutting.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doggo.Application.Services
{
    public class BreedService : AppService, IBreedService
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedService(IBreedRepository breedRepository, IMapper mapper, IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        public async Task<BreedDto> Add(BreedDto dto)
        {
            var breed = new Breed(dto.Name, dto.Type, dto.Family, dto.Origin, dto.DateOfOrigin, dto.OtherNames);

            if (!breed.IsValid())
            {
                await SendDomainNotification(breed.ValidationResult.Errors);
                return null;
            }

            _breedRepository.Add(breed);
            await _breedRepository.UnitOfWork.Commit();
            return _mapper.Map<BreedDto>(breed);
        }

        public async Task<IEnumerable<BreedDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<BreedDto>>(await _breedRepository.GetAll());
        }

        public async Task<BreedDto> Get(Guid UniqueId)
        {
            return _mapper.Map<BreedDto>(await _breedRepository.Get(UniqueId));
        }

        public async Task<BreedDto> Update(Guid UniqueId, BreedDto dto)
        {
            var mapped = _mapper.Map<Breed>(dto);
            var current = await _breedRepository.Get(UniqueId);

            if (!current.CopyDataFrom(mapped)) return _mapper.Map<BreedDto>(current);

            if (!current.IsValid())
            {
                await SendDomainNotification(current.ValidationResult.Errors);
                return null;
            }

            _breedRepository.Update(current);
            await _breedRepository.UnitOfWork.Commit();
            return _mapper.Map<BreedDto>(current);
        }

        public async Task<bool> Exist(Guid uniqueId)
        {
            return await _breedRepository.Any(p => p.UniqueId == uniqueId);
        }

        public void Dispose()
        {
            _breedRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
