using System;
using System.ComponentModel.DataAnnotations;

namespace TheaterApplication.WebApi.PostModels
{
    public class ApproveUserPm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Guid? ApproveCode { get; set; }
    }
}
