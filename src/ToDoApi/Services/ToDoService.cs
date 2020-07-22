using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Entities;
using ToDoApi.Models;
using ToDoApi.Repositories;

namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            var results = await _toDoRepository.GetAllToDoItemsAsync();
            return results;
        }

        public async Task AddToDoItemAsync(ToDoModel newItem)
        {
            await _toDoRepository.AddToDoItemAsync(newItem);
        }

        public async Task<ToDoModel> GetToDoItemByIdAsync(int id)
        {
            var result = await _toDoRepository.GetToDoItemById(id);
            return result;
        }
    }
}