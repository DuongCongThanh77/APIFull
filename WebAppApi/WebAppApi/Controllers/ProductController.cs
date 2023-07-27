using Common.ExceptionHandle;
using Data.ModelView;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(this.productService.GetAll());
        }
        [HttpGet("Page/{page:int}")]
        public IActionResult GetByPage(int page)
        {
            if(this.productService.GetPage(page).Count ==0 )
            {
                return NotFound();
            }
         
            return Ok(this.productService.GetPage(page));
        }

        [HttpGet("Search/{search}")]
        public IActionResult GetBySearch(string search)
        {
            return Ok(this.productService.GetbySearch(search));
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetProductById(Guid id)
        {
            if(this.productService.GetById(id) == null)
            {
                return NotFound();
            }
            return Ok(this.productService.GetById(id));
        }
        [HttpPost]
        public IActionResult Create(ProductView model)
        {
            try
            {
                var Product = this.productService.Create(model);
                return Ok(Product);
            }
            catch
            {
                return BadRequest();
            }  
        }
        [HttpPut("{id}")]
        public IActionResult Update(ProductView model, Guid id)
        {
            try
            {
                var Product = this.productService.Update(model,id);
                return Ok(Product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete( Guid id)
        {
            try
            {
                this.productService.Delete(id);
                return Ok();
            }
            catch(NotFoundException e)
            {
               
                return NotFound();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
