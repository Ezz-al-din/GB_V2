using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities.Test;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class TestController : BaseApiController
    {
        private readonly SmartyContext _context;
        private readonly IMapper _mapper;
        public TestController(SmartyContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(Test test)
        {
            // Save the test and its questions in the database
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return Ok(test);
        }


        [HttpPost("{testId}/submit")]
        public async Task<IActionResult> SubmitTest(int testId, List<QuestionSubmission> submissions)
        {
            // Retrieve the test from the database
            var test = await _context.Tests
                .Include(t => t.questions)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
            {
                return NotFound();
            }

            // Calculate the score for each question based on the submitted answers
            int score = 0;
            foreach (var submission in submissions)
            {
                var question = test.questions.FirstOrDefault(q => q.Id == submission.QuestionId);
                if (question != null && question.correctAnswer == submission.SelectedAnswer)
                {
                    score++;
                }
            }

            // You can save the score in the database or perform any other desired action

            return Ok(score);
        }
        [HttpGet("{testId}")]
        public async Task<IActionResult> GetTest(int testId)
        {
            // Retrieve the test from the database
            var test = await _context.Tests
                .Include(t => t.questions)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _context.Tests
                .Include(t => t.questions)
                .ToListAsync();

            return Ok(tests);
        }

        [HttpDelete("{testId}")]
        public async Task<IActionResult> DeleteTest(int testId)
        {
            // Retrieve the test from the database
            var test = await _context.Tests
                .Include(t => t.questions)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}