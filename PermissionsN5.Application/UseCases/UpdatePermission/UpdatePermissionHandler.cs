using MediatR;
using PermissionsN5.Domain.Entity;
using PermissionsN5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.UpdatePermission
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, UpdatePermissionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdatePermissionResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(request.Id);
            if(permission is null)
                throw new ArgumentNullException(nameof(permission), "No se encontró el permiso con el ID especificado.");

            var permissionToUpdate = MapToPermission(permission,request);
            await _unitOfWork.PermissionRepository.UpdateAsync(permissionToUpdate);
            return MapToUpdatePermissionResponse(permissionToUpdate);
        }

        private UpdatePermissionResponse MapToUpdatePermissionResponse(Permission permission)
        {
            return new UpdatePermissionResponse
            {
                Id = permission.Id,
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId
            };
        }

        private Permission MapToPermission(Permission permission, UpdatePermissionCommand request)
        {
            permission.PermissionTypeId = request.PermissionTypeId;
            permission.EmployeeLastName = request.EmployeeLastName;
            permission.EmployeeName = request.EmployeeName;
            return permission;
        }
    }
}
