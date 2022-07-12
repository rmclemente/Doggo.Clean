using MediatR;

namespace Domain.Core.SeedWork;

public class Query : Message, IRequest<QueryResult>
{
}
