using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Core;
using ToDo.DataAccess;
using ToDo.DTOs;
using ToDo.Entities;

namespace ToDo.Business.Concrete
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TodoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReturnModel<IEnumerable<TodoDTO>>> GetAllTodosAsync()
        {
            var todos = await _context.Todos.Include(t => t.Category).ToListAsync();
            return new ReturnModel<IEnumerable<TodoDTO>>
            {
                Success = true,
                Message = "Todos retrieved successfully.",
                Data = _mapper.Map<IEnumerable<TodoDTO>>(todos)
            };
        }

        public async Task<ReturnModel<TodoDTO>> GetTodoByIdAsync(Guid id)
        {
            var todo = await _context.Todos.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return new ReturnModel<TodoDTO>
                {
                    Success = false,
                    Message = "Todo not found",
                    Data = null
                };
            }

            return new ReturnModel<TodoDTO>
            {
                Success = true,
                Message = "Todo retrieved successfully.",
                Data = _mapper.Map<TodoDTO>(todo)
            };
        }

        public async Task<ReturnModel<TodoDTO>> CreateTodoAsync(TodoDTO todoDto)
        {
            var todo = _mapper.Map<Todo>(todoDto);
            todo.CreatedDate = DateTime.Now;
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return new ReturnModel<TodoDTO>
            {
                Success = true,
                Message = "Todo created successfully.",
                Data = _mapper.Map<TodoDTO>(todo)
            };
        }

        public async Task<ReturnModel<string>> UpdateTodoAsync(Guid id, TodoDTO todoDto)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return new ReturnModel<string>
                {
                    Success = false,
                    Message = "Todo not found",
                    Data = null
                };
            }

            _mapper.Map(todoDto, todo);
            await _context.SaveChangesAsync();

            return new ReturnModel<string>
            {
                Success = true,
                Message = "Todo updated successfully.",
                Data = "Update successful"
            };
        }

        public async Task<ReturnModel<string>> DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return new ReturnModel<string>
                {
                    Success = false,
                    Message = "Todo not found",
                    Data = null
                };
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return new ReturnModel<string>
            {
                Success = true,
                Message = "Todo deleted successfully.",
                Data = "Delete successful"
            };
        }
    }
}
