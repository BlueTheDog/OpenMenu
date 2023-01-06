using Application.Location;
using Application.Services;
using Common.Contracts;
using Common.Helpers;
using Domain.Location;
using Domain.Location.Dto;
using FluentAssertions;
using Infrastructure.DbContexts;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMenu.Tests.External.Infrastructure;
public class LocationRepositoryTests
{
    //[Fact]
    //public async Task GetLocationsAsync_OrderByName()
    //{
    //    var data = new List<LocationEntity>
    //    {
    //        new LocationEntity { Id = 1, Name = "Location A", Description = "Location A Description"},
    //        new LocationEntity { Id = 2, Name = "Location B", Description = "Location B Description"},
    //        new LocationEntity { Id = 3, Name = "Location C", Description = "Location C Description"}
    //    }.AsQueryable();
    //    var mockSet = new Mock<DbSet<LocationEntity>>();

    //    // This thing .....?
    //    mockSet.As<IQueryable<LocationEntity>>().Setup(m => m.Provider).Returns(data.Provider);
    //    mockSet.As<IQueryable<LocationEntity>>().Setup(m => m.Expression).Returns(data.Expression);
    //    mockSet.As<IQueryable<LocationEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //    mockSet.As<IQueryable<LocationEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
    //    var mockContext = new Mock<OpenMenuContext>();
    //    mockContext.Setup(c => c.Locations).Returns(mockSet.Object);

    //    var mockPropertyMappingService = new Mock<PropertyMappingService>();
    //    var service = new LocationRepository(mockContext.Object, mockPropertyMappingService.Object);
    //    var locationResourceParameters = new LocationResourceParameters
    //    {
    //        SearchQuery = "Location B",
    //        OrderBy = "Name",
    //        PageNumber = 1,
    //        PageSize = 10
    //    };
    //    var locations = await service.GetLocationsAsync(locationResourceParameters);
    //}
    }
    //[Fact]
    //public async Task GetLocationsAsync_Filter()
    //{
    //    var options = new DbContextOptionsBuilder<OpenMenuContext>()
    //        .UseInMemoryDatabase(databaseName: "TestDb")
    //        .Options;
    //    using (var context = new OpenMenuContext(options))
    //    {
    //        context.Locations.Add(new LocationEntity { Id = 1, Name = "Location A", Description = "Location A Description" });
    //        context.Locations.Add(new LocationEntity { Id = 2, Name = "Location B", Description = "Location B Description" });
    //        context.Locations.Add(new LocationEntity { Id = 3, Name = "Location C", Description = "Location C Description" });
    //        context.SaveChanges();
    //    }
    //    var mockPropertyMappingService = new Mock<PropertyMappingService>();
    //    using (var context = new OpenMenuContext(options))
    //    {
    //        var locationResourceParameters = new LocationResourceParameters
    //        {
    //            SearchQuery = "Location B",
    //            OrderBy = "Name",
    //            PageNumber = 1,
    //            PageSize = 10
    //        };
    //        var locationRepository = new LocationRepository(context, mockPropertyMappingService.Object);
    //        // Act
    //        var result = await locationRepository.GetLocationsAsync(locationResourceParameters);
    //        Assert.Single(result);
    //    }
