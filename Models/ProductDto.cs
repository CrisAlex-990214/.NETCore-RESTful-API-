using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Models
{
    //Representation Class Product Model
    public class ProductDto
    {
        //Atributtes shown 
        public Guid id { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public string purchasedDate { get; set; }
    }
}
