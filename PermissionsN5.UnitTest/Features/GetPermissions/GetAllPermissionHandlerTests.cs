using Moq;
using PermissionsN5.Application.UseCases.GetAllPermissions;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.UnitTests.Features.GetPermissions
{
    public class GetAllPermissionHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetAllPermissionHandler _handler;

        public GetAllPermissionHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetAllPermissionHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPermissionsList_WhenPermissionsExist()
        {
            var permissions = new List<Permission>
            {
                new Permission { Id = 1, EmployeeName = "John", EmployeeLastName = "Doe", PermissionDate = DateTime.UtcNow, PermissionTypeId = 2 },
                new Permission { Id = 2, EmployeeName = "Jane", EmployeeLastName = "Smith", PermissionDate = DateTime.UtcNow, PermissionTypeId = 1 }
            };

            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetAllAsync())
                           .ReturnsAsync(permissions);

            var query = new GetAllPermissionQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result.First().EmployeeName.ShouldBe("John");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoPermissionsExist()
        {
            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetAllAsync())
                           .ReturnsAsync(new List<Permission>());

            var query = new GetAllPermissionQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }
    }
}
