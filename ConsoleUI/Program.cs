﻿using Business.Concrete;
using DataAccess.Abstact;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{

    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetAllByUnitPrice(10,100))
            {
                Console.WriteLine(product.ProductName+ " = " + product.UnitPrice);
            }
        }
    }
}