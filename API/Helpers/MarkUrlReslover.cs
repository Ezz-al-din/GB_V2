using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MarkUrlReslover : IValueResolver<Mark, MarkToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public MarkUrlReslover(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Mark source, MarkToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}