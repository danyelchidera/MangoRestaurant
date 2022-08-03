using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
    public class ProductMaps: Profile
    {
        public ProductMaps()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
