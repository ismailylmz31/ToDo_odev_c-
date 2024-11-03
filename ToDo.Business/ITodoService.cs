using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;
using ToDo.DTOs;

namespace ToDo.Business
{
    public interface ITodoService
    {
        Task<ReturnModel<IEnumerable<TodoDTO>>> GetAllTodosAsync();
        Task<ReturnModel<TodoDTO>> GetTodoByIdAsync(Guid id);
        Task<ReturnModel<TodoDTO>> CreateTodoAsync(TodoDTO todoDto);
        Task<ReturnModel<string>> UpdateTodoAsync(Guid id, TodoDTO todoDto);
        Task<ReturnModel<string>> DeleteTodoAsync(Guid id);
    }
}
