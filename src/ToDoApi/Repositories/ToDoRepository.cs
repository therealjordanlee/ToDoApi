using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Entities;
using ToDoApi.Exceptions;
using ToDoApi.Models;

namespace ToDoApi.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _toDoContext;
        private readonly IMapper _mapper;

        public ToDoRepository(ToDoContext toDoContext, IMapper mapper)
        {
            _toDoContext = toDoContext;
            _mapper = mapper;
        }

        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            var results = await _toDoContext.ToDoItems.ToListAsync();
            return results;
        }

        public async Task AddToDoItemAsync(ToDoModel newItem)
        {
            try
            {
                await _toDoContext.AddAsync(new ToDoItem
                {
                    Summary = newItem.Summary,
                    Description = newItem.Description
                });
                await _toDoContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed trying to write to database: {ex.Message}");
            }
        }

        public async Task<ToDoModel> GetToDoItemById(int id)
        {
            try
            {
                var result = await _toDoContext.ToDoItems.SingleAsync(item => item.Id == id);
                return _mapper.Map<ToDoModel>(result);
            }
            catch
            {
                throw new NotFoundException($"{id} not found");
            }
        }

        public async Task DeleteToDoItemById(int id)
        {
            var item = await _toDoContext.ToDoItems.SingleOrDefaultAsync(item => item.Id == id);
            if (item == null)
            {
                throw new NotFoundException($"{id} does not exist");
            }

            _toDoContext.Remove(item);
            await _toDoContext.SaveChangesAsync();
        }
    }
}