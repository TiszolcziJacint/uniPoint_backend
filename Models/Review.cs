﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniPoint_backend.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [ForeignKey("UserId")]
        public User? Reviewer { get; set; }
        [Required]
        public int UserId { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        [Required]
        public int ServiceId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Score { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        [Timestamp]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
