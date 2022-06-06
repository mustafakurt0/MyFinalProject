using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public record ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryDal _categoryService;

        public ProductManager(IProductDal productDal, ICategoryDal categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(
                _productDal.GetAll(p => p.UnitPrice <= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(p => p.ProductId == productId);
            return new SuccessDataResult<Product>(result , "Product Getirildi");
        }

        [SecuredOperation("admin,product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //IResult result = BusinessRules.Run();

            //if (result != null)
            //{
            //    return result;
            //}

            _productDal.Add(product);
            return new SuccessResult("Eklendi");


            
        }

        public IResult AddTransactionalTest(Product product)
        {
            
        }

        private IResult ExistCategoryCount()
        {
            var categoryNumber = _categoryService.GetAll().Count;
            if (categoryNumber >= 15)
            {
                return new ErrorResult("Kategori sayısı 15'den fazla olduğu için yeni ürün eklenemez");
            }
            return new SuccessResult();

        }

        private IResult CountProductOfCategory(int categoryId)
        {
            var stokAdedi = _productDal.GetAll(p => p.CategoryId == categoryId);
            if (stokAdedi.Count >= 10)
            {
                return new ErrorResult("Bu kategorida maksimum sayıda ürün bulunmaktadır");
            }
            return new SuccessResult();
        }

        private IResult CheckSameProductName(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if (result)
            {
                return new ErrorResult("Aynı isimle ürün bulunmaktadır");
            }
            return new SuccessResult();
        }
    }
}
