using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Business;
using ToDo.Core;
using ToDo.DTOs;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _todoService.GetAllTodosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _todoService.GetTodoByIdAsync(id);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TodoDTO todoDto)
        {
            var result = await _todoService.CreateTodoAsync(todoDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TodoDTO todoDto)
        {
            var result = await _todoService.UpdateTodoAsync(id, todoDto);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _todoService.DeleteTodoAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var result = await _todoService.GetAllTodosAsync();
            return Ok(result);
        }
    }
}
