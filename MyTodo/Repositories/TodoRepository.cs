using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyTodo.Models;

namespace MyTodo.Repositories
{
	public class TodoRepository : IBaseRepository<Todo>
	{

        Models.AppContext _todoConext;
		public TodoRepository(Models.AppContext context)
		{
            this._todoConext = context;
		}

        public Todo Create(Todo value)
        {
            _todoConext.Todos.Add(value);
            _todoConext.SaveChanges();
            return value;
        }

        public async Task<Todo?> Delete(int id)
        {
            var todo = await _todoConext.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
                return null;
            _todoConext.Todos.Remove(todo);
            await _todoConext.SaveChangesAsync();
            return todo;
        }

        public Task<Todo?> Find(Expression<Func<Todo, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> FindAll()
        {
            return _todoConext.Todos;
        }

        public async Task<IEnumerable<Todo>> FindAll(Expression<Func<Todo, bool>> pred)
        {
            var todos = _todoConext.Todos.Where(pred).ToList();
            return todos;
        }

        public Todo Update(Todo value)
        {
            throw new NotImplementedException();
        }

        
    }
}

