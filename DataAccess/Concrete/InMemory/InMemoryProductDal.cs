using DataAccess.Abstact;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Oracle,Sql Server, Postgres, MongoDb
            _products = new List<Product> {
                new Product{ProductId = 1, CategoryId =1, ProductName = "Cup" , UnitPrice = 5 , UnitsInStock = 15},
                new Product{ProductId = 2, CategoryId =1, ProductName = "Vase" , UnitPrice = 500 , UnitsInStock = 3},
                new Product{ProductId = 3, CategoryId =2, ProductName = "Camera" , UnitPrice = 1500 , UnitsInStock = 2},
                new Product{ProductId = 4, CategoryId =2, ProductName = "Phone" , UnitPrice = 2040 , UnitsInStock = 65},
                new Product{ProductId = 5, CategoryId =2, ProductName = "HeadPhone" , UnitPrice = 600 , UnitsInStock = 1},
    
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // LINQ => Language Integrated Query
            // => means lambda

            Product productDelete = null; // Product productDelete = new Product() => no need to do that
/*
            foreach (var prod in _products)
            {
                if(product.ProductId == prod.ProductId)
                {
                    productDelete = prod;
                }
            }
*/
            //Lets simplelized forech code with using LINQ
            productDelete = _products.SingleOrDefault(prod=>prod.ProductId == product.ProductId);

            _products.Remove(productDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetbyCategory(int categoryId)
        {
            return _products.Where(prod=>prod.CategoryId == categoryId).ToList();  
                             //where(koşul).ToList() koşula uyanları getirip listelemek
         }

        public void Update(Product product)
        {
            Product productUpdate = null;

            productUpdate = _products.SingleOrDefault(prod => prod.ProductId == product.ProductId);
            productUpdate.ProductName = product.ProductName;
            productUpdate.UnitPrice = product.UnitPrice;
            productUpdate.UnitsInStock = product.UnitsInStock;
            productUpdate.CategoryId = product.CategoryId;

        }
    }
}
