using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Entities
{
    public enum TYPE
    {
        Good,
        Vehicle,
        Ground,
        Apartment
    }



    public class Product
    {
        //Attributes

        [Key]
        public Guid id { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(200)]
        public string description { get; set; }


        [Required]
        [Range(0, Double.MaxValue,
             ErrorMessage = "Value for {0} must be positive into the valid range.")]
        public double value { get; set; }

        [Required]
        public DateTimeOffset purchasedDate { get; set; }

        [Required]
        public bool status { get; set; }

    }
}
