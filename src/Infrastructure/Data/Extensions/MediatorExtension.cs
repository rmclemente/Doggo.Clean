using Domain.Seedwork;
using Infrastructure.Data.Context;
using MediatR;

namespace Infrastructure.Data.Extensions
{
    /// <summary>
    /// Extensions to read and dispatch all events inside a DomainEvents list from entities in the change tracker, using Mediator.
    /// </summary>
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, ApplicationDbContext context, CancellationToken cancellationToken = default)
        {
            //Get all entries in ChangeTracker that implements IEventDispatcher
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any());

            //Get all DomainEvents in inside previous entities
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            //Clean up all domain events from previous entities
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            //Dispatch all selected domainEvents through Mediator.
            //Parallel
            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent, cancellationToken);
            });

            await Task.WhenAll(tasks);

            //Single
            //foreach (var @event in domainEvents
            //    await mediator.Publish(@event, cancellationToken);
        }
    }
}
