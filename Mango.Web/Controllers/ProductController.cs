using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _prodService;

        public ProductController(IProductService prodService)
        {
            _prodService = prodService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();

            var response = await _prodService.GetAllProductsAsync<ResponseDto>();

            if(response != null)
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
           if(ModelState.IsValid)
            {
                var response = await _prodService.CreateProductAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int productId)
        {
           
              var response = await _prodService.GetProductByIdAsync<ResponseDto>(productId);

            if (response != null)
            {
                var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }
                
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {

            var response = await _prodService.GetProductByIdAsync<ResponseDto>(productId);

            if (response != null)
            {
                var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(productDto);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {

            if (ModelState.IsValid)
            {
                var response = await _prodService.UpdateProductAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
                var response = await _prodService.DeleteProductAsync<ResponseDto>(model.ProductId);

                if (response != null && response.IsSuccess)
                    return RedirectToAction(nameof(ProductIndex));
           
            return View(model);
        }
    }
}
