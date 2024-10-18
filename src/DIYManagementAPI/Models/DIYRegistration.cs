﻿using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DIYRegistration
    {
        [Key]
        public int DIYEveningID {  get; set; } 

        public string? CustomerName { get; set; }

        public string? Reparations {  get; set; }
    }
}
