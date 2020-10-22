using System;

namespace TheaterApplication.WebApi.PostModels
{
    public class PerformancePosterPm
    {
        public DateTime EventDate { get; set; }
        public int DifferenceFromStartDays { get; set; }
    }
}
