using Grpc.Core;
using MediatR;
using MoneyPlan.Contracts;
using MoneyPlan.Domain.Models;
using MoneyPlay.Api.Features.Goals;
using Period = MoneyPlan.Domain.Models.Period;
using Significance = MoneyPlan.Domain.Models.Significance;
using Status = MoneyPlan.Domain.Models.Status;

namespace MoneyPlay.Api.Services;


public class MoneyGoalsService : MoneyGoalsGrpcService.MoneyGoalsGrpcServiceBase
{
    private readonly IMediator _mediator;

    public MoneyGoalsService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override async Task<CreateGoalResponse> CreateGoal(CreateGoalRequest request, ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new CreateGoal.Command(new MoneyGoal
            {
                Amount = (decimal)request.Amount,
                AccountId = Guid.Empty,
                State = (Status)request.State,
                Title = request.Title,
                Period = (Period)request.Period,
                Significance = (Significance)request.Significance
            }));
            
            return new CreateGoalResponse
            {
                Success = true
            };
        }
        catch (Exception)
        {
            return new CreateGoalResponse 
            { 
                Success = false
            };
        }
    }
}