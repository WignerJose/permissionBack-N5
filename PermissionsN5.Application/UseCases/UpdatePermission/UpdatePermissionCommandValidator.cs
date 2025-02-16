using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.UseCases.UpdatePermission
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(permission => permission.Id).NotEmpty().NotNull();
            RuleFor(permission => permission.EmployeeName).NotEmpty();
            RuleFor(permission => permission.EmployeeLastName).NotEmpty();
            RuleFor(permission => permission.PermissionTypeId).NotEmpty().NotNull();
        }
    }
}
