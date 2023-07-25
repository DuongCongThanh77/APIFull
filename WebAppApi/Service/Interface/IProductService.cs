using Data.Model;
using Data.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService
    {
        public List<Product> GetAll();
        public List<Product> GetPage(int Page);
        public Product GetById(Guid id);
        public Product Create(ProductView product);
        public Product Update(ProductView product,Guid ProductId);
        public void Delete(Guid id);

    }
}
