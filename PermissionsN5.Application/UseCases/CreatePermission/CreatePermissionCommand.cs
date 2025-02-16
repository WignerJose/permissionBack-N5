using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.CreatePermission
{
    public class CreatePermissionCommand : IRequest<CreatePermissionResponse>
    {
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }

        public CreatePermissionCommand(string employeeName, string employeeLastName, int permissionTypeId) 
        {
            PermissionTypeId = permissionTypeId;
            EmployeeName = employeeName;
            EmployeeLastName = employeeLastName;
        }
    }

    public class CreatePermissionRequest
    {
        public string EmployeeName { get; set; } = String.Empty;
        public string EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }
    }
    public class CreatePermissionResponse
    {
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
