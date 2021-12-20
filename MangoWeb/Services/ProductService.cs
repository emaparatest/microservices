using System.Net.Http;
using System.Threading.Tasks;
using MangoWeb.Models;
using MangoWeb.Services.IServices;

namespace MangoWeb.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<T> GetAllProductsAsync<T>() =>
            await SendAsync<T>(new ApiRequest
            {
                Url = SD.ProductAPIBase + "/api/products/",
                Token = "",
                ApyType = SD.ApiType.GET
            });

        public async Task<T> GetProductByIdAsync<T>(int id) =>
            await SendAsync<T>(new ApiRequest
            {
                Url = SD.ProductAPIBase + "/api/products/" + id,
                Token = "",
                ApyType = SD.ApiType.GET
            });

        public async Task<T> CreateProductAsync<T>(ProductDto productDto) =>
            await SendAsync<T>(new ApiRequest
            {
                Data = productDto, 
                Url = SD.ProductAPIBase + "/api/products",
                Token = "",
                ApyType = SD.ApiType.POST
            });

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto) =>
            await SendAsync<T>(new ApiRequest
            {
                Data = productDto, 
                Url = SD.ProductAPIBase + "/api/products",
                Token = "",
                ApyType = SD.ApiType.PUT
            });

        public async Task<T> DeleteProductAsync<T>(int id) =>
            await SendAsync<T>(new ApiRequest
            {
                Url = SD.ProductAPIBase + "/api/products/" + id,
                Token = "",
                ApyType = SD.ApiType.DELETE
            });
    }
}