﻿using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
        }
        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "api/products" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            
            var response =  await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "api/products/" + id,
                AccessToken = ""
            });

            return response;
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }
    }
}