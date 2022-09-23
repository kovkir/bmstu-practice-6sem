using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cars.Models
{
    public class Category
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Description { set; get; }
    }
}
