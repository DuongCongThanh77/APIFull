using AutoMapper;
using Common.ExceptionHandle;
using Data.ContexDb;
using Data.Model;
using Data.ModelView;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implements
{
    public class ProductSevice : IProductService
    {
        public readonly SaleContexDb contexDb;
        public readonly IMapper map;

        public ProductSevice(SaleContexDb contexDb,IMapper map)
        {
            this.contexDb = contexDb;
            this.map = map;
        }

        public Product Create(ProductView product)
        {
            Product productCreate =   this.map.Map<Product>(product);
            productCreate.ProductID = Guid.NewGuid();
            this.contexDb.Add(productCreate);
            this.contexDb.SaveChanges();
            return productCreate;
        }

        public void Delete(Guid id)
        {
            Product p = this.contexDb.Products.SingleOrDefault(p => p.ProductID == id);
            if (p == null)
            {
                throw new NotFoundException();
             }
            else
            this.contexDb.Products.Remove(p);
            this.contexDb.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return this.contexDb.Products.ToList();
        }

        public List<Product> GetbySearch(string search)
        {
            return this.contexDb.Products.Where(p => p.ProductName.Contains(search)).ToList();
        }

        public Product GetById(Guid id)
        {
            return this.contexDb.Products.SingleOrDefault(p=>p.ProductID == id);
        }

        public List<Product> GetPage(int Page)
        {
            return this.contexDb.Products.Skip((Page - 1) * 3).Take(3).ToList();
        }

        public Product Update(ProductView product, Guid ProductId)
        {
            Product p = this.contexDb.Products.SingleOrDefault(p => p.ProductID == ProductId);
            p.ProductName = product.ProductName;
            p.Price = product.Price;
            p.Description = product.Description;
            this.contexDb.Update(p);
            this.contexDb.SaveChanges();
            return p;
            
        }
    }
}
