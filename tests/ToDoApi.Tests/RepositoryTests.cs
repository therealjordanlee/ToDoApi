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
