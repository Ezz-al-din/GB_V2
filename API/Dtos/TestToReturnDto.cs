using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TestToReturnDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}