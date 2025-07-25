﻿namespace CS330_Fall2024_FinalProject.Models
{
    public class SkiStatsViewModel
    {
        public string? StatName { get; set; }
        public double Value { get; set; }
        public string? AthleteName { get; set; }

        public IFormFile? ProfilePicture { get; set; }
        public byte[]? ProfilePictureBytes { get; set; } // For display
    }

}
