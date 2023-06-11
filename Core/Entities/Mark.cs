using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Mark : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public MarkType MarkType { get; set; }
        public int MarkTypeId { get; set; }
        public MarkBrand MarkBrand { get; set; }
        public int MarkBrandId { get; set; }
    }
}