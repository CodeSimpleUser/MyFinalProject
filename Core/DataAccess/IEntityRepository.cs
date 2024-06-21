using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //Generic Constraint -kısıtlama-
    //Class referance tip herhangi biri olabilir o yüzden
    //IEntitiy veriyoruz ki sadece bizim objelerimizi kabul etsin
    //Lakin bu seferde IEntitiy koyulabilir olucaktır ama bu da olmaması lazım çünkü spesifize  ediyoruz
    //new() ise newlenebilirleri kabul etmesini sağlayacaktır yani Interface kabul etmeyecektir
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null);
        T Get(Expression<Func<T,bool>>filter=null);
        void Add(T entitiy);
        void Update(T entitiy);
        void Delete(T entitiy);
    }
}
