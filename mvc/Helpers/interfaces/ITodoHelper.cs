using mvc.DTOs.Todo;
using mvc.Models;

namespace mvc.Helpers.interfaces
{
    public interface ITodoHelper
    {
        Task<IEnumerable<TodoItem>> GetAllByUser(int userId);
        Task<TodoItem?> GetById(int id, int userId);
        Task<bool> Create(TodoDto dto, int userId);
        Task<bool> Update(int id, TodoDto dto, int userId);
        Task<bool> ToggleComplete(int id, int userId);
        Task<bool> Delete(int id, int userId);
    }
}
