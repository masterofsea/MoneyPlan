using MoneyPlan.Contracts;
using MoneyPlan.Domain.Models;
using MoneyPlan.Intefaces;
using Period = MoneyPlan.Domain.Models.Period;
using Significance = MoneyPlan.Domain.Models.Significance;
using Status = MoneyPlan.Domain.Models.Status;

namespace MoneyPlan.Infrastructure;

public class GrpcMoneyGoalsService : IGoalsService
{
    private readonly MoneyGoalsGrpcService.MoneyGoalsGrpcServiceClient _client;

    public GrpcMoneyGoalsService(MoneyGoalsGrpcService.MoneyGoalsGrpcServiceClient client)
    {
        _client = client;
    }
    
    public async Task AddGoal(MoneyGoal goal)
    {
        var result = await _client.CreateGoalAsync(new CreateGoalRequest
        {
            Amount = (double)goal.Amount,
            Period = goal.Period switch
            {
                Period.Once => Contracts.Period.Once,
                Period.Daily => Contracts.Period.Daily,
                Period.Monthly => Contracts.Period.Monthly,
                _ => throw new ArgumentOutOfRangeException()
            },
            Significance = goal.Significance switch
            {
                Significance.Required => Contracts.Significance.Required,
                Significance.Wishful => Contracts.Significance.Wishful,
                _ => throw new ArgumentOutOfRangeException(),
            },
            State = goal.State switch
            {
                Status.Continuation => Contracts.Status.Continuation,
                Status.Cancelled => Contracts.Status.Cancelled,
                Status.Waiting => Contracts.Status.Waiting,
                Status.Completed => Contracts.Status.Completed,
                _ => throw new ArgumentOutOfRangeException()
            },
            Title = goal.Title,
        });
    }

    public Task UpdateGoal(MoneyGoal goal)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<MoneyGoal>> GetGoals()
    {
        throw new NotImplementedException();
    }
}