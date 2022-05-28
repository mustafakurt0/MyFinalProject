using System;
using System.Collections.Generic;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product();
        }

        private static void Category()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void Product()
        {
            EfProductDal efProductDal = new EfProductDal();
            IProductManager productManager = new IProductManager(efProductDal);

            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine("Product Name : " + product.ProductName + "--Category Name : " + product.CategoryName);
            }
        }
    }
}
