﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IProductService
	{
		IDataResult<List<Product>> GetAll();
		IResult Add(Product product);
		IDataResult<List<Product>> GetAllByCategoryId(int id);
		IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);
		IDataResult<Product> GetById(int id);
		IDataResult<List<ProductDetailDto>> GetProductDetails();

	}
}
