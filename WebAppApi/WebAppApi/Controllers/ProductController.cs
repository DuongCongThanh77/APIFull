using AutoMapper;
using Data.ContexDb;
using Data.ModelView;
using Microsoft.AspNetCore.Mvc;
using Service.Implements;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppApi.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("GetProductAll")]
        public IActionResult Index()
        {
            return Ok(this.productService.GetAll());
        } 
        [HttpPost("CreateProduct")]
        public IActionResult Create(ProductView model)
        {
            return Ok(this.productService.Create(model));
        }
    }
}
