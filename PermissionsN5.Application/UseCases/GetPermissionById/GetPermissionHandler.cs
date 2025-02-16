using MediatR;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.GetPermissionById
{
    public class GetPermissionHandler : IRequestHandler<GetPermissionQuery, GetPermissionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionHandler(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public async Task<GetPermissionResponse> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(request.Id);

            if(permission is null)
                throw new ArgumentNullException(nameof(permission), "No se encontró el permiso con el ID especificado.");

            return MapToGetPermissionResponse(permission);
                
        }

        private GetPermissionResponse MapToGetPermissionResponse(Permission permission)
        {
            return new GetPermissionResponse
            {
                Id = permission.Id,
                EmployeeLastName = permission.EmployeeLastName,
                EmployeeName = permission.EmployeeName,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId
            };
        }
    }
}
