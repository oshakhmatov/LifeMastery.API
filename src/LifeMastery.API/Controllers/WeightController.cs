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

    [HttpPut("records")]
    public async Task UpdateWeightRecord(
        [FromServices] PutWeightRecord putWeightRecord,
        [FromBody] PutWeightRecordRequest request,
        CancellationToken cancellationToken)
    {
        await putWeightRecord.Execute(request, cancellationToken);
    }

    [HttpDelete("records/{date}")]
    public async Task RemoveWeightRecord(
        [FromServices] RemoveWeightRecord removeWeightRecord,
        string date,
        CancellationToken cancellationToken)
    {
        await removeWeightRecord.Execute(date, cancellationToken);
    }

    [HttpPost("health-info")]
    public async Task PutHealthInfo(
        [FromServices] PutHealthInfo putHealthInfo,
        [FromBody] PutHealthInfoRequest request,
        CancellationToken cancellationToken)
    {
        await putHealthInfo.Execute(request, cancellationToken);
    }
}