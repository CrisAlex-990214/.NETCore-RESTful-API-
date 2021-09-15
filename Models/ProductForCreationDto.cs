using AutoMapper;
using Shop.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    

    //Validation Atrribute
    [TypeDescriptionPurchasedDate]
    public class ProductForCreationDto
    {

        //Atributtes shown 
        [Required(ErrorMessage = "You should fill out a type")]
        public string type { get; set; }

        [Required(ErrorMessage = "You should fill out a description")]
        [MinLength(20)]
        [MaxLength(200)]
        public string description { get; set; }
        [Required]
        [Range(0, Double.MaxValue,
             ErrorMessage = "Value must be positive into the valid range")]
        public double value { get; set; }
        [Required]
        public DateTimeOffset purchasedDate { get; set; }

    }
}
