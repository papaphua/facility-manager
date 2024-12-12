using FacilityManager.Application.Contracts;
using FacilityManager.Domain.Core.Paging;
using FacilityManager.Presentation.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacilityManager.Presentation.Controllers;

[ApiController]
[Route("contracts")]
public sealed class ContractController(
    IContractService contractService)
{
    [HttpGet]
    public async Task<IResult> Get([FromQuery] PagingQuery? query)
    {
        var result = await contractService.GetAllAsync(query);

        return result.IsSuccess
            ? Results.Ok(result.Value!.ToPagedResponse())
            : result.ToProblemDetails();
    }

    [HttpPost]
    public async Task<IResult> Create([FromForm] ContractCreationDto dto)
    {
        var result = await contractService.CreateAsync(dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}