using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _Context;

        public DbInitializer(StoreDbContext context)
        {
            _Context = context;

        }



        public async Task InitializeAsync()
        {
            try
            {
                // Create DB If It Dose nOt Exist && ApplY To Any Pending MIGRATION
                if (_Context.Database.GetPendingMigrations().Any())
                {
                    await _Context.Database.MigrateAsync();
                }

                // Data Seeding
                /// Seeding ProductType From Json Files

                if (!_Context.ProductTypes.Any())
                {


                    /// 1. Read All Data Froom brands Json File As String
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    /// 2. Transform String to C# Objects [List<Product>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    /// 3. Add List<productbrand> to DB
                    if (types is not null && types.Any())
                    {
                        await _Context.ProductTypes.AddRangeAsync(types);
                        await _Context.SaveChangesAsync();
                    }

                }

                /// Seeding ProductBrand From Json Files
                if (!_Context.ProductBrands.Any())
                {


                    /// 1. Read All Data Froom brands Json File As String
                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    /// 2. Transform String to C# Objects [List<Product>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                    /// 3. Add List<productbrand> to DB
                    if (brands is not null && brands.Any())
                    {
                        await _Context.ProductBrands.AddRangeAsync(brands);
                        await _Context.SaveChangesAsync();
                    }

                }
                /// Seeding Product From Json Files

                if (!_Context.Products.Any())
                {


                    /// 1. Read All Data Froom brands Json File As String
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    /// 2. Transform String to C# Objects [List<Product>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    /// 3. Add List<productbrand> to DB
                    if (products is not null && products.Any())
                    {
                        await _Context.Products.AddRangeAsync(products);
                        await _Context.SaveChangesAsync();
                    }

                }

            }
            catch (Exception)
            {
                throw;

            }

        }
    }
}
