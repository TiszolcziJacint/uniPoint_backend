﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniPoint_backend.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? Provider { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ServiceName { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Price { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
