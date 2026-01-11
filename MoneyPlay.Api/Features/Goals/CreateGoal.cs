using MediatR;
using MoneyPlan.Domain.Models;
using MoneyPlay.Api.Repositories;

namespace MoneyPlay.Api.Features.Goals;

public static class CreateGoal
{
    public record Command(MoneyGoal Goal) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid>
    {
        private readonly ILogger<Handler> _logger;
        private readonly ApplicationContext _context;

        public Handler(ApplicationContext context, ILogger<Handler> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.MoneyGoals.Add(request.Goal);
            
            await _context.SaveChangesAsync(cancellationToken);

            return request.Goal.Id ?? Guid.Empty;
        }
    }
}