using MediatR;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Domain.Interfaces.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PermissionsN5.Application.UseCases.UpdatePermission
{
    public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, UpdatePermissionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService<Permission> _elasticsearchService;

        public UpdatePermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService<Permission> elasticsearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
        }
        public async Task<UpdatePermissionResponse> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(request.Id);
            if(permission is null)
                throw new ArgumentNullException(nameof(permission), "No se encontró el permiso con el ID especificado.");

            var permissionToUpdate = MapToPermission(permission,request);

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var permissionUpdated = await _unitOfWork.PermissionRepository.UpdateAsync(permissionToUpdate);
            var response = await _elasticsearchService.UpdatePermissionAsync(permissionUpdated);
            await _unitOfWork.Complete();
            if (!response.IsValid)
                throw new ArgumentException("Ocurri un error al registrar en el proovedor elasticsearch");

            transaction.Complete();
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
