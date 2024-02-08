using LifeMastery.Core.Modules.Jobs.Commands;
using LifeMastery.Core.Modules.Jobs.DataTransferObjects;
using LifeMastery.Core.Modules.Jobs.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LifeMastery.API.Controllers;

[Route("api/jobs")]
public class JobsController : ApiControllerBase
{
    [HttpGet]
    public async Task<JobsViewModel> GetJobsData([FromServices] GetJobs getJobs)
    {
        return await getJobs.Execute();
    }

    [HttpPut]
    public async Task PutJob(
        [FromServices] PutJob updateJob,
        [FromBody] PutJobRequest request,
        CancellationToken cancellationToken)
    {
        await updateJob.Execute(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task Delete(
        [FromServices] RemoveJob removeJob,
        int id,
        CancellationToken cancellationToken)
    {
        await removeJob.Execute(id, cancellationToken);
    }
}