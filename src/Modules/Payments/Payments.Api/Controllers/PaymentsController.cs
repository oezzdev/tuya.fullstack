using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Application.Commands.Pay;
using Payments.Application.Queries.GetPaymentsByUser;
using Shared.Api.Extensions;
using Shared.Application.Cqrs;
using Shared.Application.Results;
using System.Security.Claims;

namespace Payments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Pay(PayCommand command)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim?.Value))
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);
        Result result = await mediator.Send<PayCommand>(command with { UserId = userId });
        return result.Match(Ok, ResultsExtensions.HandleError);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPaymentsByUserResult>>> GetMyPayments()
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim?.Value))
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);
        Result<IEnumerable<GetPaymentsByUserResult>> result = await mediator.Send<GetPaymentsByUserQuery, IEnumerable<GetPaymentsByUserResult>>(new GetPaymentsByUserQuery(userId));
        return result.Match(Ok, ResultsExtensions.HandleError);
    }
}