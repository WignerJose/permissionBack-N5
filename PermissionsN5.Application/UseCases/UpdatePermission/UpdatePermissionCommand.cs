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
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }
        public UpdatePermissionCommand(int id, String employeeName, String employeeLastName, int permissionTypeId ) 
        {
            Id = id;
            EmployeeName = employeeName;
            EmployeeLastName = employeeLastName;
            PermissionTypeId = permissionTypeId;
        }
    }

    public class UpdatePermissionRequest
    {
        public string EmployeeName { get; set; } = String.Empty;
        public string EmployeeLastName { get; set; } = String.Empty;
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
