using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class MarksWithTypesAndBrandsSpecification : BaseSpecification<Mark>
    {
        public MarksWithTypesAndBrandsSpecification(MarksSpecParams markParams)
        : base(x =>
            (string.IsNullOrEmpty(markParams.Search) || x.Name.ToLower().Contains(markParams.Search)) &&
            (!markParams.BrandId.HasValue || x.MarkBrandId == markParams.BrandId) &&
            (!markParams.TypeId.HasValue || x.MarkTypeId == markParams.TypeId)
        )
        {
            AddInclude(x => x.MarkBrand);
            AddInclude(x => x.MarkType);
            AddOrderBy(x => x.Name);
            ApplyPaging(markParams.PageSize * (markParams.PageIndex - 1),
                markParams.PageSize);
            if (!string.IsNullOrEmpty(markParams.sort))
            {
                switch (markParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public MarksWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.MarkBrand);
            AddInclude(x => x.MarkType);
        }
    }
}