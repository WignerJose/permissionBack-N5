using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PermissionsN5.Application.Exceptions;
using PermissionsN5.Application.UseCases.UpdatePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UpdatePermissionCommand>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandleExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
