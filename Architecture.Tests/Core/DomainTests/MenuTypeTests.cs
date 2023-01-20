using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Domain.Entities.Location;
using Domain.Entities.ClientType;

namespace OpenClient.Tests.Core.DomainTests;
public class ClientTypeTests
{
    [Fact]
    public void ShouldCreateClientTypeEntityWithName()
    {
        // Arrange
        string name = "Pizza";

        // Act
        var clientType = new ClientTypeEntity(name);

        // Assert
        clientType.Name.Should().Be(name);
    }

    [Fact]
    public void ShouldCreateClientTypeEntityWithEmptyName()
    {
        // Arrange
        string name = "";

        // Act
        var clientType = new ClientTypeEntity(name);

        // Assert
        clientType.Name.Should().Be(name);
    }

    [Fact]
    public void ShouldCreateClientTypeEntityWithLocations()
    {
        // Arrange
        string name = "Pizza";
        var locations = new List<LocationEntity>()
            {
                new LocationEntity("Chicago"),
                new LocationEntity("New York")
            };

        // Act
        var clientType = new ClientTypeEntity(name);
        clientType.Locations = locations;

        // Assert
        clientType.Locations.Should().BeEquivalentTo(locations);
    }

    [Fact]
    public void ShouldCreateClientTypeEntityWithoutLocations()
    {
        // Arrange
        string name = "Pizza";

        // Act
        var clientType = new ClientTypeEntity(name);

        // Assert
        clientType.Locations.Should().BeEmpty();
    }
    [Fact]
    public void ClientTypeEntity_ShouldHaveRequiredName()
    {
        // Arrange
        var clientTypeEntity = new ClientTypeEntity();

        // Act
        var validationContext = new ValidationContext(clientTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(clientTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().Contain(x => x.ErrorMessage == "The Name field is required.");
    }

    [Fact]
    public void ClientTypeEntity_ShouldHaveNameWithMaximumLengthOf50()
    {
        // Arrange
        var clientTypeEntity = new ClientTypeEntity { Name = new string('*', 51) };

        // Act
        var validationContext = new ValidationContext(clientTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(clientTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void ClientTypeEntity_ShouldBeValid_WhenNameIsProvided()
    {
        // Arrange
        var clientTypeEntity = new ClientTypeEntity { Name = "Client Type" };

        // Act
        var validationContext = new ValidationContext(clientTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(clientTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void ClientTypeEntity_ShouldInitializeLocationsCollection()
    {
        // Arrange & Act
        var clientTypeEntity = new ClientTypeEntity();

        // Assert
        clientTypeEntity.Locations.Should().NotBeNull();
        clientTypeEntity.Locations.Should().BeEmpty();
    }

    [Fact]
    public void ClientTypeEntity_ShouldHaveLocationsPropertyOfTypeICollection()
    {
        // Arrange & Act
        var clientTypeEntity = new ClientTypeEntity();

        // Assert
        clientTypeEntity.Locations.Should().BeAssignableTo<ICollection<ClientTypeEntity>>();
    }
}
