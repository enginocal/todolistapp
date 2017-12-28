using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTodoList.NoteModule
{
    interface ITodoItemRepository<TEntity> where TEntity : class
    {
        bool IsExist(Guid id);
        Task<IEnumerable<TEntity>> All(Guid userId);
        Task<TEntity> Find(Guid id);
        Task Insert(TodoItem entity);
        Task Delete(Guid id);
        Task Update(TodoItem note);
    }
}
