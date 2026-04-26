using mvc.Data;
using mvc.DTOs.Todo;
using mvc.Helpers.interfaces;
using mvc.Models;

namespace mvc.Helpers
{
    public class TodoHelper : ITodoHelper
    {
        private readonly MyAppContext _context;

        public TodoHelper(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllByUser(int userId)
        {
            var sql = @"SELECT Id, Title, Description, IsCompleted, CreatedAt, UserId
                        FROM MyAppSchema.TodoItems
                        WHERE UserId = @UserId
                        ORDER BY CreatedAt DESC";

            return await _context.LoadDataAsync<TodoItem>(sql, new { UserId = userId });
        }

        public async Task<TodoItem?> GetById(int id, int userId)
        {
            var sql = @"SELECT Id, Title, Description, IsCompleted, CreatedAt, UserId
                        FROM MyAppSchema.TodoItems
                        WHERE Id = @Id AND UserId = @UserId";

            return await _context.LoadDataSingleAsync<TodoItem>(sql, new { Id = id, UserId = userId });
        }

        public async Task<bool> Create(TodoDto dto, int userId)
        {
            var sql = @"INSERT INTO MyAppSchema.TodoItems (Title, Description, UserId)
                        VALUES (@Title, @Description, @UserId)";

            return await _context.ExecuteSqlAsync(sql, new
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            });
        }

        public async Task<bool> Update(int id, TodoDto dto, int userId)
        {
            var sql = @"UPDATE MyAppSchema.TodoItems
                        SET Title = @Title, Description = @Description
                        WHERE Id = @Id AND UserId = @UserId";

            return await _context.ExecuteSqlAsync(sql, new
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId
            });
        }

        public async Task<bool> ToggleComplete(int id, int userId)
        {
            var sql = @"UPDATE MyAppSchema.TodoItems
                        SET IsCompleted = ~IsCompleted
                        WHERE Id = @Id AND UserId = @UserId";

            return await _context.ExecuteSqlAsync(sql, new { Id = id, UserId = userId });
        }

        public async Task<bool> Delete(int id, int userId)
        {
            var sql = @"DELETE FROM MyAppSchema.TodoItems
                        WHERE Id = @Id AND UserId = @UserId";

            return await _context.ExecuteSqlAsync(sql, new { Id = id, UserId = userId });
        }
    }
}
