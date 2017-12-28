using SimpleTodoList.NoteModule;
using SimpleTodoList.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTodoList.ListModule
{
    [Table(name: "lists")]
    public class ListModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<TodoItem> Notes { get; set; }
    }
}
