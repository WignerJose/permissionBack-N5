using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<UpdatePermissionResponse>
    {
        public UpdatePermissionRequest Request { get; set; }
        public UpdatePermissionCommand(UpdatePermissionRequest updatePermissionRequest) 
        {
            Request = updatePermissionRequest;
        }
    }

    public class UpdatePermissionRequest
    {
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }
    }

    public class UpdatePermissionResponse
    {
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
