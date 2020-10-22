using System.ComponentModel.DataAnnotations;

namespace TheaterApplication.WebApi.PostModels
{
    public class PerformancePm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int DurationMinutes { get; set; }
    }
}
