using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MarkRepository : IMarkRepository
    {
        private readonly SmartyContext _context;
        public MarkRepository(SmartyContext context)
        {
            _context = context;
        }



        public async Task<Mark> GetMarkByIdAsync(int id)
        {
            return await _context.Marks
            .Include(p => p.MarkBrand)
            .Include(p => p.MarkType)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Mark>> GetMarksAsync()
        {
            return await _context.Marks
            .Include(p => p.MarkBrand)
            .Include(p => p.MarkType)
            .ToListAsync();
        }
        public async Task<IReadOnlyList<MarkBrand>> GetMarkBrandsAsync()
        {
            return await _context.MarkBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<MarkType>> GetMarkTypesAsync()
        {
            return await _context.MarkTypes.ToListAsync();
        }
    }
}