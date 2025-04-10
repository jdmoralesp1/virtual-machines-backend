using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Aplication.VirtualMachines.V1.Commands;
using PruebaTecnica.Aplication.VirtualMachines.V1.Queries;
using System.Threading.Tasks;

namespace PruebaTecnica.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class VirtualMachineController : ControllerBase
    {
        private readonly IMediator mediator;

        public VirtualMachineController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateVirtualMachineCommand command)
        {
            var response = await mediator.Send(command);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [Authorize(Roles = "Administrator,Client")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new GetAllVirtualMachinesQuery());
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [Authorize(Roles = "Administrator,Client")]
        [HttpGet("GetById/{virtualMachineId}")]
        public async Task<IActionResult> GetById(int virtualMachineId)
        {
            var response = await mediator.Send(new GetVirtualMachineByIdQuery(virtualMachineId));
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("Update/{virtualMachineId}")]
        public async Task<IActionResult> Update([FromBody] UpdateVirtualMachineCommand command, int virtualMachineId)
        {
            command.VirtualMachineId = virtualMachineId;

            var response = await mediator.Send(command);

            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [Authorize(Roles = "Administrator,Client")]
        [HttpDelete("DeleteById/{virtualMachineId}")]
        public async Task<IActionResult> DeleteById(int virtualMachineId)
        {
            var response = await mediator.Send(new DeleteVirtualMachineCommand(virtualMachineId));
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }
    }
}
