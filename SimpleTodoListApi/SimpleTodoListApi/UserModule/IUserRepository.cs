using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTodoList.UserModule
{
    interface IUserRepository
    {
        Task<bool> IsExist(string emailAddres);
        IEnumerable<User> All { get; }
        Task<User> Find(Guid id);
        Task Insert(User entity);
        Task Delete(Guid id);
        Task Update(User note);
    }
}
