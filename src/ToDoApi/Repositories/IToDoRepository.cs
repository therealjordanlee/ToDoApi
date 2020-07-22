using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Entities;
using ToDoApi.Models;

namespace ToDoApi.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDoItem>> GetAllToDoItemsAsync();

        Task AddToDoItemAsync(ToDoModel newItem);

        Task<ToDoModel> GetToDoItemById(int id);
        Task DeleteToDoItemById(int id);
    }
}