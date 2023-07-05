using Microsoft.EntityFrameworkCore;
using TraversalApi.Entities;

namespace TraversalApi.DAL
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OK3QKVJ;Initial Catalog=TraversalApi;Integrated Security=true;");
        }

        public DbSet<Visitor> Visitors { get; set; }
    }
}
