using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Entities;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllToDoItemsAsync();

        Task AddToDoItemAsync(ToDoModel newItem);

        Task<ToDoModel> GetToDoItemByIdAsync(int id);
        Task DeleteToDoItemByIdAsync(int id);
        Task UpdateToDoItemByIdAsync(int id, ToDoModel update);
    }
}