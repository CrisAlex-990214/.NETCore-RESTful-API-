using AutoMapper;
using Shop.API.Entities;
using Shop.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Shop.API.ValidationAttributes
{
    public class TypeDescriptionPurchasedDate : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {

            if (validationContext.ObjectInstance is ProductForCreationDto)
            {
                var product = (ProductForCreationDto)validationContext.ObjectInstance;


                if (product.type == product.description)
                {
                    return new ValidationResult("Type should be different from the description");
                }


                if (!Enum.IsDefined(typeof(TYPE), product.type))
                {
                    return new ValidationResult("Type should be Apartment, Good, Vehicle or Ground");
                }

                if(product.purchasedDate > DateTimeOffset.Now)
                {
                    return new ValidationResult("Purchased Date shouldn´t be greater than Actual Date");
                }
            }

            else if (validationContext.ObjectInstance is IEnumerable<ProductForCreationDto>)
            {
                var products = (IEnumerable<ProductForCreationDto>) validationContext.ObjectInstance;

                foreach (var product in products)
                {
                    if (product.type == product.description)
                    {
                        return new ValidationResult("Type should be different from the description");
                    }


                    if (!Enum.IsDefined(typeof(TYPE), product.type))
                    {
                        return new ValidationResult("Type should be Apartment, Good, Vehicle or Ground");
                    }

                    if (product.purchasedDate > DateTimeOffset.Now)
                    {
                        return new ValidationResult("Purchased Date shouldn´t be greater than Actual Date");
                    }
                }
            }

            else if (validationContext.ObjectInstance is ProductForUpdateDto)
            {
                var product = (ProductForUpdateDto)validationContext.ObjectInstance;

                if (product.type == product.description)
                {
                    return new ValidationResult("Type should be different from the description");
                }


                if (!Enum.IsDefined(typeof(TYPE), product.type))
                {
                    return new ValidationResult("Type should be Apartment, Good, Vehicle or Ground");
                }
            }

            
            
            return ValidationResult.Success;
        }

    }
}
