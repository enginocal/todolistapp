using SimpleTodoList.NoteModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTodoList.UserModule
{
    [Table(name: "users")]
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
