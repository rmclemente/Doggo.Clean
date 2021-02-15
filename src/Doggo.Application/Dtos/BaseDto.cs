using System;

namespace Doggo.Application.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public Guid UniqueId { get; set; }
    }
}
