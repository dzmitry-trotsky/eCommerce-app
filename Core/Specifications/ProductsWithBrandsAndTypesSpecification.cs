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
        public ProductsWithBrandsAndTypesSpecification(ProductSpecParams productParams)
           : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(_ => _.ProductType);
            AddInclude(_ => _.ProductBrand);
            AddOrderBy(_ => _.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
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
