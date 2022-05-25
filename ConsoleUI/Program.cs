using System;
using System.Collections.Generic;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InMemoryProductDal inMemoryProductDal = new InMemoryProductDal();
            IProductManager productManager = new IProductManager(inMemoryProductDal);

            while (true)
            {
                Console.WriteLine("****************************");
                Console.WriteLine("İşlem");
                string input = Console.ReadLine();
                if (input == "get")
                {
                    foreach (Product product in productManager.GetAll())
                    {
                        Console.WriteLine(product.ProductName);
                    }
                }
                else if (input =="")
                {
                    Console.WriteLine("Balance  : 18");
                }

            }

        }
    }
}
