using MediatR;
using PermissionsN5.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.GetAllPermissions
{
    public class GetAllPermissionHandler : IRequestHandler<GetAllPermissionQuery, IList<GetAllPermissionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPermissionHandler(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<GetAllPermissionResponse>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetAllAsync();
            return permissions.Select((p) => new GetAllPermissionResponse
            {
                Id = p.Id,
                EmployeeLastName = p.EmployeeName,
                EmployeeName = p.EmployeeName,
                PermissionDate = p.PermissionDate,
                PermissionTypeId = p.PermissionTypeId,
            }).ToList();
        }
    }
}
