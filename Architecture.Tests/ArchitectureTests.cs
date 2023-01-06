using NetArchTest.Rules;
using Web;

namespace Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "Web";

        [Fact]
        public void Domain_Should_Not_Have_DependencyOnAnyOtherProject()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApplicationNamespace, 
                InfrastructureNamespace, 
                PresentationNamespace, 
                WebNamespace
            };

            // Act
            var testRestult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testRestult.IsSuccessful.Equals(true);
        }

        [Fact]
        public void Application_Should_Not_Have_DependencyOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamespace, 
                PresentationNamespace, 
                WebNamespace
            };

            // Act
            var testRestult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testRestult.IsSuccessful.Equals(true);
        }
        [Fact]
        public void Infrastructure_Should_Not_Have_DependencyOnOtherProject()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var testRestult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testRestult.IsSuccessful.Equals(true);
        }

        [Fact]
        public void Presentation_Should_Not_Have_DependencyOnOtherProject()
        {
            // Arrange
            var assembly = typeof(AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var testRestult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testRestult.IsSuccessful.Equals(true);
        }
    }
}