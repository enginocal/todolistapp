using Microsoft.EntityFrameworkCore;
using SimpleTodoList.Infrastracture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTodoList.UserModule
{
    public class UserRepository : IUserRepository
    {
        private readonly SimpleTodoListDbContext _context;
        public UserRepository()
        {
            _context = new SimpleTodoListDbContext(new DbContextOptions<SimpleTodoListDbContext>());
            //_context = new SimpleTodoListDbContext();
        }
        public IEnumerable<User> All => throw new NotImplementedException();

        public async Task<bool> IsExist(string emailAddres)
        {
            return await _context.Users.AnyAsync(x => x.EmailAddress == emailAddres);
        }

        public async Task<User> Find(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Find(string emailAddress)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
        }

        public async Task Insert(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Find(id);
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task Update(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
