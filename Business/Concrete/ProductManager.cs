﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		IProductDal _productDal;
		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}

		[ValidationAspect(typeof(ProductValidator))]
		public IResult Add(Product product)
		{
			_productDal.Add(product);
			return new Result(true, Messages.ProductAdded);
		}

		public IDataResult<List<Product>> GetAll()
		{
			if(DateTime.UtcNow.Minute == 17)
			{
				return new ErrorDataResult<List<Product>>();
			}
			return new DataResult<List<Product>>(_productDal.GetAll(), true);
		}

		public IDataResult<List<Product>> GetAllByCategoryId(int id)
		{
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
		}

		public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
		{
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
		}

		public IDataResult<Product> GetById(int id)
		{
			return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
		}

		public IDataResult<List<ProductDetailDto>> GetProductDetails()
		{
			return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
		}

	}
}
