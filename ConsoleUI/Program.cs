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
            
            EfProductDal  efProductDal = new EfProductDal();
            IProductManager productManager = new IProductManager(efProductDal);

            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
            

        }
    }
}
