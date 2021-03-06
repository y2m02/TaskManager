using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? EndDate { get; set; }

        public int AssignmentId { get; set; }

        public int? StatusId { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [ForeignKey(nameof(AssignmentId))]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual Status Status { get; set; }
    }
}
