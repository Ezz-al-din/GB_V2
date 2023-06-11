using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.Test
{
    public class Test
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<Question> questions { get; set; }
    }
}