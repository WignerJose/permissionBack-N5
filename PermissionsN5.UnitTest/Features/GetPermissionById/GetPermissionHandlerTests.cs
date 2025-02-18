using Moq;
using PermissionsN5.Application.UseCases.GetPermissionById;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.UnitTests.Features.GetPermissionById
{
    public class GetPermissionHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetPermissionHandler _handler;

        public GetPermissionHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetPermissionHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPermission_WhenPermissionExists()
        {
            var permission = new Permission { Id = 1, EmployeeName = "John", EmployeeLastName = "Doe", PermissionDate = DateTime.UtcNow, PermissionTypeId = 2 };

            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetByIdAsync(1))
                           .ReturnsAsync(permission);

            var query = new GetPermissionQuery(1);

            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
            result.EmployeeName.ShouldBe("John");
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenPermissionDoesNotExist()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetByIdAsync(99))
                           .ReturnsAsync((Permission)null);

            var query = new GetPermissionQuery(99);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(async () =>
            {
                await _handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
