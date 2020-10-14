using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int TaskId { get; set; }

        public int? StatusId { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual Status Status { get; set; }
    }
}
