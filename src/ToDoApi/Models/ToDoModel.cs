using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class ToDoModel
    {
        [Required]
        [StringLength(50)]
        public string Summary { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public bool Completed { get; set; }
    }
}