using AutoMapper;
using Library.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using Shop.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductCollectionsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductCollectionsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// Add a product collection in the shop
        [HttpPost()]
        public ActionResult<IEnumerable<ProductDto>> AddProductCollection([FromBody] IEnumerable<ProductForCreationDto> productCollection)
        {
            var products = _mapper.Map<IEnumerable<Entities.Product>>(productCollection);
            
            
            foreach(var product in products)
            _productRepository.AddProduct(product);

            if (!_productRepository.Save())
                throw new Exception("Creating an product failed on save.");


            var productCollectionToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);

            var idsAsString = string.Join(",",
                productCollectionToReturn.Select(a => a.id));

            return CreatedAtRoute("GetProductCollection",
                new { ids = idsAsString },
                productCollectionToReturn);
        }

        // Get an author collection

        [HttpGet("({ids})", Name = "GetProductCollection")]
        public IActionResult GetAuthorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var products = _productRepository.GetProducts(ids);

            if (ids.Count() != products.Count())
            {
                return NotFound();
            }

            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsToReturn);
        }

    }
}
