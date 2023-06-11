using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IMarkRepository
    {
        Task<Mark> GetMarkByIdAsync(int id);
        Task<IReadOnlyList<Mark>> GetMarksAsync();
        Task<IReadOnlyList<MarkBrand>> GetMarkBrandsAsync();
        Task<IReadOnlyList<MarkType>> GetMarkTypesAsync();
    }
}