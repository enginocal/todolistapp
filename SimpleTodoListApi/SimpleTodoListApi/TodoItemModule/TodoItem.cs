using SimpleTodoList.UserModule;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTodoList.NoteModule
{
    [Table(name: "todoitems")]
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; }
        public string Note { get; set; }
        //public Guid ListId { get; set; }
        //[ForeignKey("ListId")]
        //public ListModel List { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(name: "UserId")]
        public User User { get; set; }
        public bool IsCompleted { get; set; }
    }
}
