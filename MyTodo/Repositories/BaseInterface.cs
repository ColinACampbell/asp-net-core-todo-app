using System;
using System.Linq.Expressions;
namespace MyTodo.Repositories
{
	public interface IBaseRepository<T> where T: class
	{
		T Create(T value);
		T Update(T value);
		T Delete(T value);
		IEnumerable<T> FindAll();
		Task<T?> Find(Expression<Func<T, bool>> pred);
		Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> pred);
	}
}

