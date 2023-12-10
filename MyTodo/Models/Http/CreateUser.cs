namespace MyTodo.Models.Http;

class CreateUser
{
    public string email { get; set; }
    public string password { get; set; }
    public string username { get; set; }

    public CreateUser()
    {
    }
}