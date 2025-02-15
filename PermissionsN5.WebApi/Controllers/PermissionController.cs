using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsN5.Application.UseCases.GetAllPermissions;

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
    }
}
