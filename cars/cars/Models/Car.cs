using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cars.Models
{
    public class Car
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Brand { set; get; }

        [Required]
        public string Model { set; get; }

        [Required]
        public uint Price { set; get; }

        [ForeignKey("Category")]
        public int CategoryId { set; get; }
    }

    public enum CarSortState
    {
        IdAsc,
        IdDesc,

        BrandAsc,
        BrandDesc,

        ModelAsc,
        ModelDesc,

        CategoryNameAsc,
        CategoryNameDesc,

        PriceAsc,
        PriceDesc
    }
}
