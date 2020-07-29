using Microsoft.EntityFrameworkCore;
using System;
using ToDoApi.Entities;
using ToDoApi.Repositories;
using ToDoApi.Services;
using Xunit;
using AutoMapper;
using ToDoApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace ToDoApi.Tests
{
    public class RepositoryTests
    {
        private readonly Mapper _mapper;
        public RepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ToDoItem, ToDoModel>()
               .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Completed)));

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public async Task GetAllToDoItemsAsync_Returns_AllToDoRecordsInDatabase()
        {
            using (ToDoContext dbContext = RepositoryTestHelper.NewInMemoryToDoContext())
            {
                await RepositoryTestHelper.SeedData(dbContext);
                var toDoRepository = new ToDoRepository(dbContext, _mapper);

                var result = await toDoRepository.GetAllToDoItemsAsync();
                var expectedResult = RepositoryTestHelper.GetMockToDoItemList();

                result.Should().BeEquivalentTo(expectedResult);
            }
        }

        [Fact]
        public async Task GetToDoItemById_Returns_ToDoRecordFromDatabase()
        {
            using (ToDoContext dbContext = RepositoryTestHelper.NewInMemoryToDoContext())
            {
                await RepositoryTestHelper.SeedData(dbContext);
                var todoRepository = new ToDoRepository(dbContext, _mapper);
                var result = await todoRepository.GetToDoItemByIdAsync(1);
                var expectedResult = RepositoryTestHelper.GetMockToDoItemList()
                    .Where(x => x.Id == 1)
                    .Select(x => new ToDoModel { Summary = x.Summary, Description = x.Description, Completed = x.Completed })
                    .FirstOrDefault();

                var actualResult = await todoRepository.GetToDoItemByIdAsync(1);
                actualResult.Should().BeEquivalentTo(expectedResult);
            }
        }

        [Fact]
        public async Task AddToDoItemAsync_Adds_RecordToDatabase()
        {
            using (ToDoContext dbContext = RepositoryTestHelper.NewInMemoryToDoContext())
            {
                var dummyToDoRecord = new ToDoModel
                {
                    Summary = "mock summary",
                    Description = "mock description",
                    Completed = false
                };

                var toDoRepository = new ToDoRepository(dbContext, _mapper);
                await toDoRepository.AddToDoItemAsync(dummyToDoRecord);

                var actualResult = await dbContext.ToDoItems.SingleAsync(x => x.Summary == dummyToDoRecord.Summary);
                var expectedResult = new ToDoItem
                {
                    Id = 1,
                    Summary = "mock summary",
                    Description = "mock description",
                    Completed = false
                };

                actualResult.Should().BeEquivalentTo(expectedResult);
            }
        }

        [Fact]
        public async Task DeleteToDoItemById_Removes_ToDoItemFromDatabase()
        {
            using (ToDoContext dbContext = RepositoryTestHelper.NewInMemoryToDoContext())
            {
                await RepositoryTestHelper.SeedData(dbContext);
                var toDoRepository = new ToDoRepository(dbContext, _mapper);
                await toDoRepository.DeleteToDoItemByIdAsync(3);

                var result = await dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == 3);
                result.Should().BeNull();
            }
        }

        [Fact]
        public async Task UpdateToDoItemById_Updates_Existing()
        {
            using (ToDoContext dbContext = RepositoryTestHelper.NewInMemoryToDoContext())
            {
                await RepositoryTestHelper.SeedData(dbContext);
                var toDoRepository = new ToDoRepository(dbContext, _mapper);
                var updateToDoModel = new ToDoModel
                {
                    Summary = "summary updated",
                    Description = "description updated",
                    Completed = true
                };

                await toDoRepository.UpdateToDoItemByIdAsync(3, updateToDoModel);
                var actualResult = await dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == 3);
                var expectedResult = new ToDoItem
                {
                    Id = 3,
                    Summary = updateToDoModel.Summary,
                    Description = updateToDoModel.Description,
                    Completed = updateToDoModel.Completed
                };

                actualResult.Should().BeEquivalentTo(expectedResult);
            }
        }
    }

    public static class RepositoryTestHelper
    {
        public static List<ToDoItem> GetMockToDoItemList()
        {
            return new List<ToDoItem>()
            {
                new ToDoItem{
                    Id = 1,
                    Summary = "Test Entry 1",
                    Description = "abc",
                    Completed = false
                    },
                    new ToDoItem{
                    Id = 2,
                    Summary = "Test Entry 2",
                    Description = "def",
                    Completed = true
                    },
                    new ToDoItem{
                    Id = 3,
                    Summary = "Test Entry 3",
                    Description = "ghi",
                    Completed = false
                    }
            };
        }

        public static ToDoContext NewInMemoryToDoContext()
        {
            string dbName = Guid.NewGuid().ToString();
            DbContextOptions<ToDoContext> options = new DbContextOptionsBuilder<ToDoContext>()
                            .UseInMemoryDatabase(databaseName: dbName).Options;
            return new ToDoContext(options);
        }

        public static async Task SeedData(ToDoContext dbContext)
        {
                var items = GetMockToDoItemList();
                await dbContext.AddRangeAsync(items);
                await dbContext.SaveChangesAsync();
        }
    }
}
