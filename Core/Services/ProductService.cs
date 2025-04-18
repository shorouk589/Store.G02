﻿using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {


            var spec = new ProductWithBrandsAndTypeSpecifications();


            // Get All Products Through ProductRepositor;
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);



            // Mappin IEnumerable<Product> To IEnumerable <ProductResultDto> : AutoMapper

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return result;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypeSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brand = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brand);
            return result;
        }


        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;


        }
    }

}
