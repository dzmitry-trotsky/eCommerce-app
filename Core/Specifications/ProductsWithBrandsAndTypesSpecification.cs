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
        public ProductsWithBrandsAndTypesSpecification(string sort, int? brandId, int? typeId)
            :base(_ => 
                (!brandId.HasValue || _.ProductBrandId == brandId) &&
                (!typeId.HasValue || _.ProductTypeId == typeId) 
            )
        {
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
            AddOrderBy(_ => _.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(_ => _.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(_ => _.Price);
                        break;
                    default:
                        AddOrderBy(_ => _.Name);
                        break;
                };
            }
        }

        public ProductsWithBrandsAndTypesSpecification(int id) : base(_ => _.Id == id)
        {
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
        }
    }
}
