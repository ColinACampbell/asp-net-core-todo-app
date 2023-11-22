using System;
using Microsoft.AspNetCore.Identity;
using MyTodo.Models;

namespace MyTodo.Repositories
{
	public class UserRepository : IBaseRepository<User>
	{
        Models.AppContext _appContext;
        IPasswordHasher<User> _passwordHasher;
		public UserRepository(Models.AppContext appContext, IPasswordHasher<User> passwordHasher)
		{
            _appContext = appContext;
            _passwordHasher = passwordHasher;
		}

        public User Create(User newUser)
        {
            string newPassword = _passwordHasher.HashPassword(newUser, newUser.password);
            newUser.password = newPassword;
            _appContext.Users.Add(newUser);
            _appContext.SaveChanges();
            return newUser;
        }

        public User Delete(User value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public User Update(User value)
        {
            throw new NotImplementedException();
        }
    }
}

