using System;
using System.Linq;
using System.Linq.Expressions;
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
            _todoConext.SaveChangesAsync();
            return value;
        }

        public Todo Delete(Todo value)
        {
            throw new NotImplementedException();
        }

        public Task<Todo?> Find(Expression<Func<Todo, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> FindAll()
        {
            return _todoConext.Todos;
        }

        public Todo Update(Todo value)
        {
            throw new NotImplementedException();
        }

        
    }
}

