using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.GetAllPermissions
{
    public class GetAllPermissionQuery : IRequest<IList<GetAllPermissionResponse>>
    {
        public GetAllPermissionQuery() { }
    }

    public class GetAllPermissionResponse
    {
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
