using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification()
        {
            AddValue(_ => _.ProductType);
            AddValue(_ => _.ProductBrand);
        }

        public ProductsWithBrandsAndTypesSpecification(int id) : base(_ => _.Id == id)
        {
            AddValue(_ => _.ProductType);
            AddValue(_ => _.ProductBrand);
        }
    }
}
