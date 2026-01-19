using Cards.Application.Commands.CreateCard;
using Cards.Application.Commands.DeleteCard;
using Cards.Application.Commands.UpdateCard;
using Cards.Application.Queries.GetCardById;
using Cards.Application.Queries.GetCardsByUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Extensions;
using Shared.Application.Cqrs;
using Shared.Application.Results;
using System.Security.Claims;

namespace Cards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CardsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreatedCardResult>> CreateCard(CreateCardCommand command)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim?.Value))
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);
        Result<CreatedCardResult> result = await mediator.Send<CreateCardCommand, CreatedCardResult>(command with { UserId = userId });
        return result.Match(Ok, ResultsExtensions.HandleError);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCard(UpdateCardCommand command)
    {
        Result result = await mediator.Send(command);
        return result.Match(Ok, ResultsExtensions.HandleError);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCardByIdResult>> GetCardById(Guid id)
    {
        Result<GetCardByIdResult> result = await mediator.Send<GetCardByIdQuery, GetCardByIdResult>(new GetCardByIdQuery(id));
        return result.Match(Ok, ResultsExtensions.HandleError);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCard(Guid id)
    {
        Result result = await mediator.Send(new DeleteCardCommand(id));
        return result.Match(NoContent, ResultsExtensions.HandleError);
    }

    [HttpGet]
    public async Task<ActionResult<List<GetCardsByUserResult>>> GetMyCards()
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim?.Value))
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);
        Result<IEnumerable<GetCardsByUserResult>> result = await mediator.Send<GetCardsByUserQuery, IEnumerable<GetCardsByUserResult>>(new GetCardsByUserQuery(userId));
        return result.Match(Ok, ResultsExtensions.HandleError);
    }
}