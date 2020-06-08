using System;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;
        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act
            testCommand.HowTo = "Execute Unit Tests";

            // Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);

        }

        [Fact]
        public void CanChangePlatform()
        {
            // Arrange

            // Act
            testCommand.Platform = "Dotnet";

            // Assert
            Assert.Equal("Dotnet", testCommand.Platform);

        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}