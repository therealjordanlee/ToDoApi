using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Entities
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItem>().HasData(new ToDoItem
            {
                Id = 1,
                Summary = "Example ToDo Item #1",
                Description = "(Seeded Data)",
                Completed = false
            },
            new ToDoItem
            {
                Id = 2,
                Summary = "Example ToDo Item #2",
                Description = "(Seeded Data)",
                Completed = false
            });
        }
    }

    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Summary { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}