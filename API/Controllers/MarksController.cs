using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MarksController : BaseApiController
    {
        private readonly IGenericRepository<Mark> _markRepo;
        private readonly IGenericRepository<MarkBrand> _markBrandRepo;
        private readonly IGenericRepository<MarkType> _markTypeRepo;
        private readonly IMapper _mapper;

        public MarksController(IGenericRepository<Mark> markRepo, IGenericRepository<MarkBrand> markBrandRepo, IGenericRepository<MarkType> markTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _markTypeRepo = markTypeRepo;
            _markRepo = markRepo;
            _markBrandRepo = markBrandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<MarkToReturnDto>>> GetMarks([FromQuery] MarksSpecParams markParams)
        {
            var spec = new MarksWithTypesAndBrandsSpecification(markParams);
            var countSpec = new MarkWithFiltersForCountSpecification(markParams);
            var totalItems = await _markRepo.CountAsync(countSpec);

            var marks = await _markRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Mark>, IReadOnlyList<MarkToReturnDto>>(marks);

            return Ok(new Pagination<MarkToReturnDto>(markParams.PageIndex, markParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarkToReturnDto>> GetMark(int id)
        {
            var spec = new MarksWithTypesAndBrandsSpecification(id);
            var mark = await _markRepo.GetEntityWithSpec(spec);

            if (mark == null) return NotFound(new ApiResponse(400));

            return _mapper.Map<Mark, MarkToReturnDto>(mark);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<MarkBrand>>> GetMarkBrands()
        {
            return Ok(await _markBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<MarkType>>> GetMarkTypes()
        {
            return Ok(await _markTypeRepo.ListAllAsync());
        }
    }
}