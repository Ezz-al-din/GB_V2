using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data
{
    public class SmartyContextSeed
    {
        public static async Task SeedAsync(SmartyContext context)
        {
            if (!context.MarkBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<MarkBrand>>(brandsData);
                context.MarkBrands.AddRange(brands);
            }

            if (!context.MarkTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<MarkType>>(typesData);
                context.MarkTypes.AddRange(types);
            }

            if (!context.Marks.Any())
            {
                var marksData = File.ReadAllText("../Infrastructure/Data/SeedData/marks.json");
                var marks = JsonSerializer.Deserialize<List<Mark>>(marksData);
                context.Marks.AddRange(marks);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}