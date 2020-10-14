using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public int StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
