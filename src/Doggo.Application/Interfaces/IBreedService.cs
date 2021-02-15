using Doggo.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doggo.Application.Interfaces
{
    public interface IBreedService : IDisposable
    {
        Task<BreedDto> Add(BreedDto dto);
        Task<IEnumerable<BreedDto>> GetAll();
        Task<BreedDto> Get(Guid uniqueId);
        Task<BreedDto> Update(Guid uniqueId, BreedDto dto);
        Task<bool> Exist(Guid uniqueId);
    }
}
