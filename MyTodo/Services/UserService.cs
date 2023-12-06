using System.Security.Claims;
using MyTodo.Models;
using MyTodo.Repositories;

namespace MyTodo.Services
{

    public interface IUserService {
        Task<User?> GetUser(ClaimsPrincipal user);
    }
    class UserService : IUserService {
        IBaseRepository<User> _userRepo;

        public UserService(){

        }
        public UserService(IBaseRepository<User> userRepo){
            _userRepo = userRepo;
        }

        public Task<User?> GetUser(ClaimsPrincipal user)
        {
            var id_string = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.ToString();
            var id = Int32.Parse(id_string);
            return _userRepo.Find(e => e.Id == id);
        }
    }
}