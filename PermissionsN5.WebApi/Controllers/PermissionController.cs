using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsN5.Application.UseCases.CreatePermission;
using PermissionsN5.Application.UseCases.GetAllPermissions;
using PermissionsN5.Application.UseCases.GetPermissionById;
using PermissionsN5.Application.UseCases.UpdatePermission;

namespace PermissionsN5.WebApi.Controllers
{
    [ApiController]

    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("permissions", Name = "getAllPermissions")]
        public async Task<ActionResult> GetPermissions()
        {
            var request = new GetAllPermissionQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("permissions/{id}", Name = "getPermission")]
        public async Task<ActionResult> GetPermission([FromRoute] int id)
        {
            var request = new GetPermissionQuery(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("permissions", Name = "CreatePermission")]
        public async Task<ActionResult> CreatePermission([FromBody] CreatePermissionCommand createPermissionCommand)
        {
            var response = await _mediator.Send(createPermissionCommand);
            return Ok(response);
        }

        [HttpPut("permissions/{id}", Name = "UpdatePermission")]
        public async Task<ActionResult> UpdatePermission([FromRoute] int id, [FromBody] UpdatePermissionRequest updatePermissionRequest)
        {
            var command = new UpdatePermissionCommand(
                                id,
                                updatePermissionRequest.EmployeeName,
                                updatePermissionRequest.EmployeeLastName,
                                updatePermissionRequest.PermissionTypeId
                            );

            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
