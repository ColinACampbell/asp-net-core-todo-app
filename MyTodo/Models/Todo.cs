﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;



namespace MyTodo.Models
{

    [Table("Todos")]
    public class Todo
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		[Column("todo_date")]
		public string date { get; set; }

        public Todo()
		{

		}
	}
}
