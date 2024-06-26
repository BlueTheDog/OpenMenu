﻿//using FluentAssertions;


//namespace OpenMenu.Tests.Core.DomainTests;
//public class LocationEntityTests
//{
//    [Fact]
//    public void ShouldCreateLocationEntityWithName()
//    {
//        // Arrange
//        string name = "Chicago";

//        // Act
//        var location = new Domain.Location.LocationEntity(name);

//        // Assert
//        location.Name.Should().Be(name);
//    }

//    [Fact]
//    public void ShouldCreateLocationEntityWithDescription()
//    {
//        // Arrange
//        string name = "Chicago";
//        string description = "A city in Illinois, USA";

//        // Act
//        var location = new Domain.Location.LocationEntity(name)
//        {
//            Description = description
//        };

//        // Assert
//        location.Description.Should().Be(description);
//    }

//    [Fact]
//    public void ShouldCreateLocationEntityWithoutDescription()
//    {
//        // Arrange
//        string name = "Chicago";

//        // Act
//        var location = new Domain.Location.LocationEntity(name);

//        // Assert
//        location.Description.Should().BeNull();
//    }

//    [Fact]
//    public void ShouldCreateLocationEntityWithMenuType()
//    {
//        // Arrange
//        string name = "Chicago";
//        var menuType = new Domain.ClientType.ClientTypeEntity("Pizza");

//        // Act
//        var location = new Domain.Location.LocationEntity(name)
//        {
//            ClientType = menuType
//        };

//        // Assert
//        location.ClientType.Should().Be(menuType);
//    }

//    [Fact]
//    public void ShouldCreateLocationEntityWithoutMenuType()
//    {
//        // Arrange
//        string name = "Chicago";

//        // Act
//        var location = new Domain.Location.LocationEntity(name);

//        // Assert
//        location.ClientType.Should().BeNull();
//    }
//}
