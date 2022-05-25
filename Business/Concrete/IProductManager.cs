using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public record IProductManager : IProductService
    {
        private IProductDal _productDal;

        public IProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //İş kodları
            return _productDal.GetAll();

        }

        //public List<Product> GetAllByCategory(int categoryId)
        //{
        //    return _productDal.GetAllByCategory(categoryId);

        //}
    }
}
