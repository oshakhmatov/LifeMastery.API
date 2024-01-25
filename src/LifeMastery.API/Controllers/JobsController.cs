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
    public async Task AddOrUpdate(
        [FromServices] UpdateJob updateJob,
        [FromBody] UpdateJobRequest request)
    {
        await updateJob.Update(request);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromServices] DeleteJob deleteJob, int id)
    {
        await deleteJob.Delete(id);
    }
}