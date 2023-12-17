using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo.Models;
using MyTodo.Repositories;
using MyTodo.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTodo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/todo")]
    public class TodoController : Controller
    {
        IBaseRepository<Todo> todoRepository;
        IUserService _userService;

        public TodoController(IBaseRepository<Todo> repo, IUserService userService)
        {
            todoRepository = repo;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<Todo>> GetTODOs()
        {
            // TODO Use user id
            var currentUser = HttpContext.User;
            var user = await _userService.GetUser(currentUser);
            return await todoRepository.FindAll(t => t.userId == user.Id);
        }

        [HttpPost]
        public async Task<Todo> CreateTODO([FromBody] MyTodo.Models.Http.CreateTodo newTodo)
        {
            Todo todo = new Todo();

            // Create special class for accepting the request
            var currentUser = HttpContext.User;
            var user = await _userService.GetUser(currentUser);

            todo.title = newTodo.title;
            todo.User = user!;
            todo.userId = user!.Id;
            todo.date = newTodo.date;
            todo.description = newTodo.description;
            return todoRepository.Create(todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTODO(string id)
        {       
            var _id = Int32.Parse(id);
            var todo = await todoRepository.Delete(_id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }
    }
}

