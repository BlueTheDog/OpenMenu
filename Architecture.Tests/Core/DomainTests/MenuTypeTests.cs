using System.Collections.Generic;
using Domain.Location;
using System.ComponentModel.DataAnnotations;
using Domain.MenuType;
using FluentAssertions;
using Xunit;

namespace OpenMenu.Tests.Core.DomainTests;
public class MenuTypeTests
{
    [Fact]
    public void ShouldCreateMenuTypeEntityWithName()
    {
        // Arrange
        string name = "Pizza";

        // Act
        var menuType = new Domain.MenuType.MenuTypeEntity(name);

        // Assert
        menuType.Name.Should().Be(name);
    }

    [Fact]
    public void ShouldCreateMenuTypeEntityWithEmptyName()
    {
        // Arrange
        string name = "";

        // Act
        var menuType = new Domain.MenuType.MenuTypeEntity(name);

        // Assert
        menuType.Name.Should().Be(name);
    }

    [Fact]
    public void ShouldCreateMenuTypeEntityWithLocations()
    {
        // Arrange
        string name = "Pizza";
        var locations = new List<Domain.Location.LocationEntity>()
            {
                new Domain.Location.LocationEntity("Chicago"),
                new Domain.Location.LocationEntity("New York")
            };

        // Act
        var menuType = new Domain.MenuType.MenuTypeEntity(name);
        menuType.Locations = locations;

        // Assert
        menuType.Locations.Should().BeEquivalentTo(locations);
    }

    [Fact]
    public void ShouldCreateMenuTypeEntityWithoutLocations()
    {
        // Arrange
        string name = "Pizza";

        // Act
        var menuType = new Domain.MenuType.MenuTypeEntity(name);

        // Assert
        menuType.Locations.Should().BeEmpty();
    }
    [Fact]
    public void MenuTypeEntity_ShouldHaveRequiredName()
    {
        // Arrange
        var menuTypeEntity = new Domain.MenuType.MenuTypeEntity();

        // Act
        var validationContext = new ValidationContext(menuTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(menuTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().Contain(x => x.ErrorMessage == "The Name field is required.");
    }

    [Fact]
    public void MenuTypeEntity_ShouldHaveNameWithMaximumLengthOf50()
    {
        // Arrange
        var menuTypeEntity = new Domain.MenuType.MenuTypeEntity { Name = new string('*', 51) };

        // Act
        var validationContext = new ValidationContext(menuTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(menuTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void MenuTypeEntity_ShouldBeValid_WhenNameIsProvided()
    {
        // Arrange
        var menuTypeEntity = new Domain.MenuType.MenuTypeEntity { Name = "Menu Type" };

        // Act
        var validationContext = new ValidationContext(menuTypeEntity, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(menuTypeEntity, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void MenuTypeEntity_ShouldInitializeLocationsCollection()
    {
        // Arrange & Act
        var menuTypeEntity = new Domain.MenuType.MenuTypeEntity();

        // Assert
        menuTypeEntity.Locations.Should().NotBeNull();
        menuTypeEntity.Locations.Should().BeEmpty();
    }

    [Fact]
    public void MenuTypeEntity_ShouldHaveLocationsPropertyOfTypeICollection()
    {
        // Arrange & Act
        var menuTypeEntity = new Domain.MenuType.MenuTypeEntity();

        // Assert
        menuTypeEntity.Locations.Should().BeAssignableTo<ICollection<MenuTypeEntity>>();
    }
}
