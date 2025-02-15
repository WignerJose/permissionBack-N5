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
            RuleFor(permission => permission.Request.Id).NotEmpty().NotNull();
            RuleFor(permission => permission.Request.EmployeeName).NotEmpty();
            RuleFor(permission => permission.Request.EmployeeLastName).NotEmpty();
            RuleFor(permission => permission.Request.PermissionTypeId).NotEmpty().NotNull();
        }
    }
}
