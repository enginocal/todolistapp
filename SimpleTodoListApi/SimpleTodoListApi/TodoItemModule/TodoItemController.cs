using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Linq;

namespace SimpleTodoList.NoteModule
{
    public class TodoItemController : NancyModule
    {
        public TodoItemController() :
            base("/api/todolist")
        {
            this.RequiresAuthentication();

            var repo = new TodoItemRepository();

            Get("/{userId:guid}", async args =>
             {
                 var userId = args.userId;
                 var todoLists = await repo.All(userId);
                 return todoLists;
             });

            Get("/{userId:guid}/{todoId:guid}", async args =>
             {
                 var userId = args.userId;
                 var todoId = args.noteId;
                 var todoItem = await repo.Find(todoId);
                 if (todoItem == null)
                 {
                     return HttpStatusCode.NotFound;
                 };
                 return todoItem;
             });

            Post("/{userId:guid}", async args =>
            {
                var userId = args.userId;
                if (userId!=getLoggedInUserId())
                {
                    return HttpStatusCode.Forbidden;
                }
                var todo = this.Bind<TodoItemAddUpdateModel>();

                var entity = new TodoItem()
                {
                    IsCompleted = false,
                    Note = todo.Note,
                    UserId = userId
                };

                repo = new TodoItemRepository();
                await repo.Insert(entity);
                return HttpStatusCode.OK;
            });

            Put("/{userId:guid}/{todoId:guid}", async args =>
            {
                var userId = args.userId;
                if (userId != getLoggedInUserId())
                {
                    return HttpStatusCode.Forbidden;
                }
                var todoId = args.todoId;

                var note = this.Bind<TodoItemAddUpdateModel>();
                repo = new TodoItemRepository();
                var existingTodoItem = await repo.Find(todoId);
                if (existingTodoItem == null)
                {
                    return new NotFoundResponse();
                }
                existingTodoItem.Note = note.Note;
                existingTodoItem.IsCompleted = note.IsCompleted;
                await repo.Update(existingTodoItem);
                return existingTodoItem;
            });

            Delete("/{userId:guid}/{todoId:guid}", async args =>
            {
                var userId = args.userId;
                if (userId != getLoggedInUserId())
                {
                    return HttpStatusCode.Forbidden;
                }
                var todoId = args.todoId;

                var existingTodoItem = await repo.Find(todoId);
                if (existingTodoItem == null)
                {
                    return new NotFoundResponse();
                }
                await repo.Delete(todoId);
                return HttpStatusCode.OK;
            }
            );            
        }

        private Guid getLoggedInUserId()
        {
            var loggedInUserId = Guid.Empty; ;
            Guid.TryParse(
          Context
            .CurrentUser
            .Claims.FirstOrDefault(c => c.Type.StartsWith("id"))
            ?.Value.Split(':').Last() ?? "",
          out loggedInUserId);
            return loggedInUserId;
        }
    }
}
