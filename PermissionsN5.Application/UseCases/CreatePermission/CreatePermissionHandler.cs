using Azure;
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

namespace PermissionsN5.Application.UseCases.CreatePermission
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, CreatePermissionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService<Permission> _elasticsearchService;

        public CreatePermissionHandler(IUnitOfWork unitOfWork, IElasticSearchService<Permission> elasticsearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
        }
        public async Task<CreatePermissionResponse> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToSave = MapToPermission(request);
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var permissionSaved = await _unitOfWork.PermissionRepository.AddAsync(permissionToSave);
            await _unitOfWork.Complete();
            var response = await _elasticsearchService.CreatePermissionAsync(permissionSaved);
            if (!response.IsValid)
                throw new ArgumentException("Ocurri un error al registrar en el proovedor elasticsearch");

            transaction.Complete();
            return MapToPermissionResponse(permissionSaved);
        }

        private CreatePermissionResponse MapToPermissionResponse(Permission permissionSaved)
        {
            return new CreatePermissionResponse
            {
                Id = permissionSaved.Id,
                EmployeeName = permissionSaved.EmployeeName,
                EmployeeLastName = permissionSaved.EmployeeLastName,
                PermissionDate = permissionSaved.PermissionDate,
                PermissionTypeId = permissionSaved.PermissionTypeId,
            };
        }

        private Permission MapToPermission(CreatePermissionCommand request)
        {
            return new Permission
            {
                EmployeeName = request.EmployeeName,
                EmployeeLastName = request.EmployeeLastName,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = DateTime.Now,
            };
        }
    }
}
