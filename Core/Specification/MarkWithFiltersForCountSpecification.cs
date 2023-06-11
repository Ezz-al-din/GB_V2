using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class MarkWithFiltersForCountSpecification : BaseSpecification<Mark>
    {
        public MarkWithFiltersForCountSpecification(MarksSpecParams markParams) : base(x =>
            (string.IsNullOrEmpty(markParams.Search) || x.Name.ToLower().Contains(markParams.Search)) &&
            (!markParams.BrandId.HasValue || x.MarkBrandId == markParams.BrandId) &&
            (!markParams.TypeId.HasValue || x.MarkTypeId == markParams.TypeId)
        )
        {

        }
    }
}