using Microsoft.EntityFrameworkCore;
using Web.Models.Entity;

namespace Web.Models.Db
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Media> Medias{get;set;}
        public DbSet<Message> Messages{get;set;}
        public DbSet<Chat> Chats{get;set;}
        public DbSet<ChatUser> ChatsUsers{get;set;}
        public DbSet<Connection> Connections{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}