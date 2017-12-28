using Microsoft.EntityFrameworkCore;
using SimpleTodoList.Infrastracture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTodoList.NoteModule
{
    public class TodoItemRepository : ITodoItemRepository<TodoItem>
    {
        private readonly SimpleTodoListDbContext _context;
        public TodoItemRepository() => _context =
            //new SimpleTodoListDbContext();
            new SimpleTodoListDbContext(new DbContextOptions<SimpleTodoListDbContext>());

        public bool IsExist(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoItem> Find(Guid id)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Insert(TodoItem entity)
        {
            await _context.Set<TodoItem>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Find(id);
            _context.Set<TodoItem>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TodoItem note)
        {
            _context.Set<TodoItem>().Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> All(Guid userId)
        {
            var notes = await _context.Notes.ToListAsync();
            return notes.FindAll(x => x.UserId == userId);
        }
    }
}
