using LifeMastery.Core.Modules.WeightControl.Commands;
using LifeMastery.Core.Modules.WeightControl.DataTransferObjects;
using LifeMastery.Core.Modules.WeightControl.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LifeMastery.API.Controllers;

[Route("api/weight-control")]
public class WeightController : ApiControllerBase
{
    [HttpGet]
    public async Task<WeightControlViewModel> GetWeightControlData(
        [FromServices] GetWeightControlData getWeightControlData)
    {
        return await getWeightControlData.Execute();
    }

    [HttpPost("records")]
    public async Task AddWeightRecord(
        [FromServices] AddWeightRecord addWeightRecord,
        [FromBody] AddWeightRecordRequest request,
        CancellationToken cancellationToken)
    {
        await addWeightRecord.Execute(request, cancellationToken);
    }

    [HttpPut("records/{date}")]
    public async Task UpdateWeightRecord(
        [FromServices] UpdateWeightRecord updateWeightRecord,
        DateOnly date,
        [FromBody] UpdateWeightRecordRequest request)
    {
        await updateWeightRecord.Execute(date, request);
    }

    [HttpDelete("records/{date}")]
    public async Task DeleteWeightRecord(
        [FromServices] DeleteWeightRecord deleteWeightRecord,
        DateOnly date)
    {
        await deleteWeightRecord.Execute(date);
    }

    [HttpPost("health-info")]
    public async Task AddOrUpdate(
        [FromServices] UpdateHealthInfo updateHealthInfo,
        [FromBody] UpdateHealthInfoRequest request,
        CancellationToken cancellationToken)
    {
        await updateHealthInfo.Execute(request, cancellationToken);
    }
}