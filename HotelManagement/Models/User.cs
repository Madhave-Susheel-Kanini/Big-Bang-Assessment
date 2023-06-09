﻿using System.ComponentModel.DataAnnotations;

namespace AssessmentAPI.Model
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserCreatedAt { get; set; }
    }
}
