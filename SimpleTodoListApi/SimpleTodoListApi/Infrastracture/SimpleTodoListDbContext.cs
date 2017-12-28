using Microsoft.EntityFrameworkCore;
using SimpleTodoList.NoteModule;
using SimpleTodoList.UserModule;

namespace SimpleTodoList.Infrastracture
{
    public class SimpleTodoListDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<ListModel> Lists { get; set; }
        public DbSet<TodoItem> Notes { get; set; }

        public SimpleTodoListDbContext(DbContextOptions<SimpleTodoListDbContext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"User ID=todolist_usr;Password=123456;Host=localhost;Port=5432;Database=todolist;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");
        }
    }
}
