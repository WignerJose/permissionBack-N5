using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.GetPermissionById
{
    public class GetPermissionQuery : IRequest<GetPermissionResponse>
    {
        public int Id { get; set; }

        internal GetPermissionQuery(int Id) 
        {
            this.Id = Id;
        }
    }

    public class GetPermissionResponse
    {
        public int Id { get; set; }
        public String EmployeeName { get; set; } = String.Empty;
        public String EmployeeLastName { get; set; } = String.Empty;
        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
