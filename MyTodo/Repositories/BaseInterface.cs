using System;
namespace MyTodo.Repositories
{
	public interface IBaseRepository<T> where T: class
	{
		T Create(T value);
		T Update(T value);
		T Delete(T value);
		IEnumerable<T> FindAll();
		T Find();
	}
}

