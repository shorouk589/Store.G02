using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandsAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();

        }
        public ProductWithBrandsAndTypeSpecifications() : base(null)
        {
            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
