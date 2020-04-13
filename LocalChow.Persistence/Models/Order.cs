using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LocalChow.Persistence.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public DateTime DatePlaced { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        public User User { get; set; }
    }
}
