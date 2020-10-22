using System;
using System.ComponentModel.DataAnnotations;

namespace TheaterApplication.WebApi.PostModels
{
    public class PerformanceSchedulePm
    {
        [Required]
        public DateTime StartAt { get; set; }
        [Required]
        public int TicketsCount { get; set; }
        [Required]
        public bool IsRepeat { get; set; }
    }
}
