using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTodo.Models;
using MyTodo.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTodo.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("/api/v1/todo")]
    public class TodoController : ControllerBase
    {
        IBaseRepository<Todo> todoRepository;

        public TodoController(IBaseRepository<Todo> repo)
        {
            todoRepository = repo;
        }

        [HttpGet]
        public IEnumerable<Todo> GetTODOs()
        {
            return todoRepository.FindAll();
        }

        [HttpPost]
        public Todo CreateTODO([FromBody] Todo newTodo){
            Console.WriteLine(newTodo.title);
            Todo todo = new Todo();
            todo.title = newTodo.title;
            todo.date = newTodo.date;
            todo.description = newTodo.description;
            return todoRepository.Create(todo);
        }
    }
}

