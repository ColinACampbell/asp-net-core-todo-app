namespace MyTodo.Models.Http;

public class CreateTodo
{

    public string title { get; set; }
    public string description { get; set; }
    public string date { get; set; }

    public CreateTodo()
    {
    }
}