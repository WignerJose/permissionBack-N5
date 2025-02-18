using Elasticsearch.Net;
using Moq;
using Nest;
using PermissionsN5.Application.UseCases.GetAllPermissions;
using PermissionsN5.Application.UseCases.UpdatePermission;
using PermissionsN5.Domain.Entities;
using PermissionsN5.Domain.Interfaces;
using PermissionsN5.Domain.Interfaces.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsN5.UnitTests.Features.UpdatePermission
{
    public class UpdatePermissionHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticSearchService<Permission>> _elasticSearchServiceMock;
        private readonly UpdatePermissionHandler _handler;

        public UpdatePermissionHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticSearchServiceMock = new Mock<IElasticSearchService<Permission>>();
            _handler = new UpdatePermissionHandler(_unitOfWorkMock.Object, _elasticSearchServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdatePermission_WhenPermissionExists()
        {
            var existingPermission = new Permission
            {
                Id = 1,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionTypeId = 2
            };

            var mockResponse = new Mock<Nest.UpdateResponse<Permission>>();
            mockResponse.Setup(r => r.IsValid).Returns(true);

            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetByIdAsync(1))
                           .ReturnsAsync(existingPermission);

            _unitOfWorkMock.Setup(u => u.PermissionRepository.UpdateAsync(It.IsAny<Permission>()))
                           .ReturnsAsync(new Permission());

            _elasticSearchServiceMock.Setup(es => es.UpdatePermissionAsync(It.IsAny<Permission>()))
                                     .ReturnsAsync(mockResponse.Object);

            var command = new UpdatePermissionCommand(1, "Jane", "Smith", 3);

            await _handler.Handle(command, CancellationToken.None);

            _unitOfWorkMock.Verify(u => u.PermissionRepository.UpdateAsync(It.Is<Permission>(p =>
                p.Id == 1 &&
                p.EmployeeName == "Jane" &&
                p.EmployeeLastName == "Smith" &&
                p.PermissionTypeId == 3
            )), Times.Once);

            _elasticSearchServiceMock.Verify(es => es.UpdatePermissionAsync(It.IsAny<Permission>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenElasticSearchFails()
        {
            // Arrange
            var existingPermission = new Permission
            {
                Id = 1,
                EmployeeName = "John",
                EmployeeLastName = "Doe",
                PermissionDate = DateTime.UtcNow,
                PermissionTypeId = 2
            };

            var mockResponse = new Mock<Nest.UpdateResponse<Permission>>();
            mockResponse.Setup(r => r.IsValid).Returns(false);  // ElasticSearch falla

            _unitOfWorkMock.Setup(u => u.PermissionRepository.GetByIdAsync(1))
                           .ReturnsAsync(existingPermission);

            _unitOfWorkMock.Setup(u => u.PermissionRepository.UpdateAsync(It.IsAny<Permission>()))
                           .ReturnsAsync(new Permission());

            _elasticSearchServiceMock.Setup(es => es.UpdatePermissionAsync(It.IsAny<Permission>()))
                                     .ReturnsAsync(mockResponse.Object);

            var command = new UpdatePermissionCommand(1, "Jane", "Smith", 3);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            Assert.Equal("Ocurri un error al registrar en el proovedor elasticsearch", exception.Message);

            _unitOfWorkMock.Verify(u => u.PermissionRepository.UpdateAsync(It.IsAny<Permission>()), Times.Once);
            _elasticSearchServiceMock.Verify(es => es.UpdatePermissionAsync(It.IsAny<Permission>()), Times.Once);
        }
    }
}
