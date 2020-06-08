using System;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CommandAPI.Controllers;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        DbContextOptionsBuilder<CommandContext> optionsBuilder;
        CommandContext dbContext;
        CommandsController controller;

        public CommandsControllerTests()
        {
            optionsBuilder = new DbContextOptionsBuilder<CommandContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemDB");
            dbContext = new CommandContext(optionsBuilder.Options);

            controller = new CommandsController(dbContext);
        }

        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var cmd in dbContext.CommandItems)
            {
                dbContext.CommandItems.Remove(cmd);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            controller = null;
        }

        // ACTION 1 Tests: GET  /api/commands
        // TEST 1.1 REQUEST OBJECTS WHEN NONE EXISTS - RETURN "NOTHING'

        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Act
            var result = controller.GetCommandItems();

            // Assert
            Assert.Empty(result.Value);
        }

        [Fact]
        public void GetCommandItemsReturnsOneItemWhenDBHasOneObject()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "Do Something",
                Platform = "Some Platform",
                CommandLine = "Some Command"
            };
            dbContext.CommandItems.Add(command);
            dbContext.SaveChanges();

            // Act
            var result = controller.GetCommandItems();

            // Assert
            Assert.Single(result.Value);
        }

        [Fact]
        public void GetCommandItemsReturnNItemsWhenDBHasNObjects()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "Do Somethting",
                Platform = "Some Platform",
                CommandLine = "Some Command"
            };
            var command2 = new Command
            {
                HowTo = "Do Somethting",
                Platform = "Some Platform",
                CommandLine = "Some Command"
            };
            dbContext.CommandItems.Add(command);
            dbContext.CommandItems.Add(command2);
            dbContext.SaveChanges();

            // Act
            var result = controller.GetCommandItems();

            // Assert
            Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public void GetCommandItemsReturnsTheCorrectType()
        {
            //Arrange


            //Act
            var result = controller.GetCommandItems();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<Command>>>(result);

        }

        [Fact]
        public void GetCommanditemReturnsNullResultWhenInvalidID()
        {
            // Arrange
            // DB should be empty, any ID will be invalid

            // Act
            var result = controller.GetCommandItem(1);

            // Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public void GetCommandItemReturns404NotFoundWhenInvalidID()
        {
            // Arrange
            // DB should be empty, any ID will be invalid

            // Act
            var result = controller.GetCommandItem(0);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandItemReturnsTheCorrectType()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "Do Something",
                Platform = "Some Platform",
                CommandLine = "Some command"
            };
            dbContext.CommandItems.Add(command);
            dbContext.SaveChanges();

            var cmdId = command.Id;

            // Act
            var result = controller.GetCommandItem(cmdId);

            // Assert
            Assert.IsType<ActionResult<Command>>(result);
        }

        [Fact]
        public void GetCommandItemReturnsTheCorrectResource()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "How to Do",
                Platform = "Some Platform",
                CommandLine = "Some Command"
            };
            dbContext.CommandItems.Add(command);
            dbContext.SaveChanges();

            var cmdId = command.Id;

            // Act
            var result = controller.GetCommandItem(cmdId);

            // Assert
            Assert.Equal(cmdId, result.Value.Id);
        }

        [Fact]
        public void PostCommandItemObjectCountIncrementWhenValidObject()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "How to",
                Platform = "platform",
                CommandLine = "command"
            };
            var oldCount = dbContext.CommandItems.Count();
            // var Count = oldCount.Value.Count();

            // Act
            var result = controller.PostCommandItem(command);

            // Assert
            Assert.Equal(oldCount + 1, dbContext.CommandItems.Count());
        }

        [Fact]
        public void PostCommandItemReturns201CreatedWhenValidObect()
        {
            // Arrange
            var command = new Command
            {
                HowTo = "Do something",
                Platform = "Some Platform",
                CommandLine = "Some command"
            };

            // Act
            var result = controller.PostCommandItem(command);

            // Asset
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

    }
}