namespace TenderManagementSystem.API.Controllers
{
    using Application.Tender.Commands;
    using Application.Tender.Queries;
    using Application.Tender.VM;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : BaseController
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<TenderVM>> GetOne(int id)
        {
            return await this.Mediator.Send(new GetOneQuery { Id = id });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<TenderVM>> GetAll()
        {
            return await this.Mediator.Send(new GetAllQuery());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> Post(AddCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<bool>> Put(UpdateCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await this.Mediator.Send(new DeleteCommand { Id = id });
        }
    }
}
