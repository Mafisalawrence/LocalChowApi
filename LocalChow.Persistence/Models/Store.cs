using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocalChow.Persistence.Models
{
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public User User { get; set; }
    }
}
