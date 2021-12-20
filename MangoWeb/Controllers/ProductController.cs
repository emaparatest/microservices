using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MangoWeb.Models;
using MangoWeb.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MangoWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        // GET
        public async Task<IActionResult> ProductIndex()
        {
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            var list = new List<ProductDto>();
            if (response != null && response.IsSuccess)
            {
                list =  JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
        
        //get
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);
            
            var response = await _productService.CreateProductAsync<ResponseDto>(productDto);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }


            return View(productDto);
        }
        
        //get
        public async Task<IActionResult> ProductUpdate(int productId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);

            if (response == null || !response.IsSuccess) return NotFound();
            
            var productDto =  JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(productDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductUpdate(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);

            var response = await _productService.UpdateProductAsync<ResponseDto>(productDto);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            
            return View(productDto);
        }
        
        //get
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);

            if (response == null || !response.IsSuccess) return NotFound();
            
            var productDto =  JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(productDto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            if (!ModelState.IsValid) return View(productDto);

            var response = await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            
            return View(productDto);
        }
        
       

    }
}