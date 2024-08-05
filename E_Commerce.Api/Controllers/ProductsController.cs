using AutoMapper;
using E_Commerce.Api.Errors;
using E_Commerce.Core;
using E_Commerce.Core.DTO;
using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        //[Cash(200)]
        [Authorize]
        public async Task<ActionResult<PaginatedResultDto<ProductToReturnDto>>> GetProducts([FromQuery] SpecsParameter specsParameter)
        {
            // Check if parameters are provided
            if (specsParameter != null)
            {
                // If parameters are provided, apply specifications
                var specs = new ProductWithBrandAndTypeSpecifications(specsParameter);
                var products = await _unitOfWork.Repository<Product>().GetAllWithSpecsAsync(specs);
                var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
                var countSpec = new ProductWithFilterationForCountSpecification(specsParameter);
                var count = await _unitOfWork.Repository<Product>().GetCountWithSpecsAsync(countSpec);
                return Ok(new PaginatedResultDto<ProductToReturnDto>(specsParameter.PageIndex, specsParameter.PageSize, count, mappedProducts));
            }
            else
            {
                // If no parameters are provided, get all products
                var allProducts = await _unitOfWork.Repository<Product>().GetAllAsync();
                var mappedAllProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(allProducts);
                return Ok(new PaginatedResultDto<ProductToReturnDto>(1, allProducts.Count, allProducts.Count, mappedAllProducts));
            }
        }

        //2. Get Specific Product By Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.Repository<Product>().GetWithSpecsAsync(specs);
            if (product is null) return NotFound(new ApiResponse(404, $"Product With Id {id} not Found"));
            var MappedProducts = _mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(MappedProducts);
        }

        // 3. GetAllBrands
        [HttpGet(("Brands"))]
        public async Task<ActionResult<ProductBrand>> GetAllBrands()
        {
            var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }
        //4. GetAllTypes
        [HttpGet("Types")]
        public async Task<ActionResult<ProductType>> GetAllTypes()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }
    }
}
