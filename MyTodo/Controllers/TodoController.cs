using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class TodoController : ControllerBase
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
            return todoRepository.FindAll();
        }

        [HttpPost]
        public async Task<Todo> CreateTODO([FromBody] Todo newTodo){
            Console.WriteLine(newTodo.title);
            Todo todo = new Todo();

            // Create special class for accepting the request
            var currentUser = HttpContext.User;
            var user = await _userService.GetUser(currentUser);

            todo.title = newTodo.title;
            todo.User = user!;
            todo.date = newTodo.date;
            todo.description = newTodo.description;
            return todoRepository.Create(todo);
        }
    }
}

