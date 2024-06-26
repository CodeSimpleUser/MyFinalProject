using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstact;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
            
        }

        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CountInvalid(product.CategoryId));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        private IResult CountInvalid(int CategoryId) 
        {
            var result = _productDal.GetAll(p => p.CategoryId == CategoryId);
            if (result == null)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryInvalid);

            }
            return new SuccessResult();
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour < 18)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintanenceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id),true,"Products are packeaged");
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max),Messages.ProductAdded);
        }

        public IDataResult<Product>GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailsDto>> GetProductDetails()
        {
            //if (DateTime.Now.Hour < 22)
            //{
            //    return new ErrorDataResult<List<ProductDetailsDto>>(Messages.MaintanenceTime);
            //}
            return new SuccessDataResult<List<ProductDetailsDto>> (_productDal.GetProductDetails());
        }
        [CacheRemoveAspect("IProductService.Get")]
        [TransactionScopeAspect()]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId);
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryInvalid);
            }
            throw new NotImplementedException();
        }

        
    }
}
