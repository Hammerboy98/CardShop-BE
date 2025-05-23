﻿using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } 
        public string Role { get; set; } 
    }
}
