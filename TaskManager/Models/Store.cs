using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual List<Assignment> Tasks { get; set; }
    }
}
