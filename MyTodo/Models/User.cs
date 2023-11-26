using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTodo.Models
{
    [Table("User")]
    public class User
	{
		[Key]
		[Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string username { get; set; }

		public User()
		{
		}
	}

	public class UserReturn: User
	{
		public string token { get; set; }

		public UserReturn(User _user)
		{
			this.email = _user.email;
			this.password = null;
			this.Id = _user.Id;
			this.username = _user.username;
		}

        public UserReturn()
        {
        }
    }
}

