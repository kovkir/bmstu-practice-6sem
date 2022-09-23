using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cars.Models
{
    public class UserCar
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
