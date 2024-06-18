using DataAccess.Abstact;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitiyFramework
{
    //NuGet

    public class EfProductDal : IProductDal
    {
        public void Add(Product entitiy)
        {
            //When the job done , it will be clear from the memory
            //It is better for performance
            //IDispsible pattern implement of C#
            using (NorthwindContext context = new NorthwindContext())
            {
                var AddedEntity = context.Entry(entitiy);
                AddedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product entitiy)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var DeletedEntitiy = context.Entry(entitiy);
                DeletedEntitiy.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (var context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();

            }
        }

        public void Update(Product entitiy)
        {
            using (var context = new NorthwindContext())
            {
                var UpdatedEntitiy = context.Entry(entitiy);
                UpdatedEntitiy.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
