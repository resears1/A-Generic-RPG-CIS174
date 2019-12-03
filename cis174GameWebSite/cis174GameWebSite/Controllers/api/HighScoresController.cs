using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cis174GameWebSite.Data;
using cis174GameWebSite.Models;

namespace cis174GameWebSite.Controllers.api
{
    [Route("api/highscores")]
    [ApiController]
    public class HighScoresController : ControllerBase
    {
        private readonly HighScoreDbContext _context;

        public HighScoresController(HighScoreDbContext context)
        {
            _context = context;
        }

        // GET: api/HighScores
        [HttpGet]
        public IEnumerable<HighScoreViewModel> GetHighScores()
        {
            return _context.HighScores;
        }

        // GET: api/HighScores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHighScoreViewModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var highScoreViewModel = await _context.HighScores.FindAsync(id);

            if (highScoreViewModel == null)
            {
                return NotFound();
            }

            return Ok(highScoreViewModel);
        }

        // PUT: api/HighScores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHighScoreViewModel([FromRoute] int id, [FromBody] HighScoreViewModel highScoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != highScoreViewModel.ScoreId)
            {
                return BadRequest();
            }

            _context.Entry(highScoreViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HighScoreViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HighScores
        [HttpPost]
        public async Task<IActionResult> PostHighScoreViewModel([FromBody] HighScoreViewModel highScoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HighScores.Add(highScoreViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHighScoreViewModel", new { id = highScoreViewModel.ScoreId }, highScoreViewModel);
        }

        // DELETE: api/HighScores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHighScoreViewModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var highScoreViewModel = await _context.HighScores.FindAsync(id);
            if (highScoreViewModel == null)
            {
                return NotFound();
            }

            _context.HighScores.Remove(highScoreViewModel);
            await _context.SaveChangesAsync();

            return Ok(highScoreViewModel);
        }

        private bool HighScoreViewModelExists(int id)
        {
            return _context.HighScores.Any(e => e.ScoreId == id);
        }
    }
}