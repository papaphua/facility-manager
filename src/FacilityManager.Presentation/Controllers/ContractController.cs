using FacilityManager.Application.Contracts;
using FacilityManager.Presentation.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacilityManager.Presentation.Controllers;

[ApiController]
[Route("contracts")]
public sealed class ContractController(
    IContractService contractService)
{
    [HttpPost]
    public async Task<IResult> Create([FromForm] ContractDto dto)
    {
        var result = await contractService.CreateAsync(dto);

        return result.IsSuccess
            ? Results.Ok()
            : result.ToProblemDetails();
    }
}