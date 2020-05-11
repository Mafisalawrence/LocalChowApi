using LocalChow.Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LocalChow.Domain.DTO
{
    public class StoreDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public Guid UserID { get; set; } //TODO : Set to required after testing
    }
}
