using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mango.Services.ProductApi.DbContexts;
using Mango.Services.ProductApi.Models;
using Mango.Services.ProductApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProductDto>> GetProducts() 
            => _mapper.Map<List<ProductDto>>(await _db.Products.ToListAsync());

        public async Task<ProductDto> GetProductById(int productId) 
            => _mapper.Map<ProductDto>(await _db.Products.FindAsync(productId));

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                await _db.Products.AddAsync(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _db.Products.FindAsync(productId);
                if (product == null) return false;
                
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }    
        }
    }
}