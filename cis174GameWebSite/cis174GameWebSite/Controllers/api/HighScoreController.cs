using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cis174GameWebSite.Data;
using cis174GameWebSite.Models;
using Microsoft.Extensions.Logging;

namespace cis174GameWebSite.Controllers.api
{
    [Route("api/highscore")]
    [ApiController]
    public class HighScoreController : ControllerBase
    {
        private readonly HighScoreDbContext _context;
        private readonly ILogger _logger;

        public HighScoreController(HighScoreDbContext context,
            ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //GET: api/HighScore
        [HttpGet]
        public IEnumerable<HighScoreViewModel> GetHighScoreViewModel()
        {
            return _context.HighScoreViewModel;
        }

        //GET: api/HighScore/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHighScoresForUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state invalid");
                return BadRequest(ModelState);
            }

            Array highScores = _context.HighScoreViewModel
                                .OrderByDescending(a => a.Score)
                                .Where(a => a.UserId == id)
                                .Select(a => new HighScoreViewModel
                                {
                                    ScoreId = a.ScoreId,
                                    Score = a.Score,
                                    UserId = a.UserId,
                                }).ToArray();

            if (highScores == null)
            {
                _logger.LogWarning($"Highscores not found {id}");
                return NotFound();
            }

            if(highScores.Length < 10) {
                return Ok(_context.HighScoreViewModel
                                .OrderByDescending(a => a.Score)
                                .Where(a => a.UserId == id)
                                .Select(a => new HighScoreViewModel
                                {
                                    ScoreId = a.ScoreId,
                                    Score = a.Score,
                                    UserId = a.UserId,
                                }));
            }

            _logger.LogInformation($"High Scores retrieved for {id}");
            return Ok(_context.HighScoreViewModel
                                .OrderByDescending(a => a.Score).Take(10)
                                .Where(a => a.UserId == id)
                                .Select(a => new HighScoreViewModel
                                {
                                    ScoreId = a.ScoreId,
                                    Score = a.Score,
                                    UserId = a.UserId,
                                }));
        }

        // POST: api/HighScore
        [HttpPut]
        [Route("addscore")]
        public async Task<IActionResult> PostHighScoreViewModel([FromBody] HighScoreViewModel highScoreViewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state invalid");
                return BadRequest(ModelState);
            }

            _context.HighScoreViewModel.Add(highScoreViewModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation("New High score successfully created ");
            return CreatedAtAction("GetHighScoreViewModel", new { id = highScoreViewModel.ScoreId }, highScoreViewModel);
        }

        // DELETE: api/HighScore/5
        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> DeleteHighScoreViewModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state invalid");
                return BadRequest(ModelState);
            }

            var highScoreViewModel = await _context.HighScoreViewModel.FindAsync(id);
            if (highScoreViewModel == null)
            {
                _logger.LogWarning($"Highscores not found {id}");
                return NotFound();
            }

            _context.HighScoreViewModel.Remove(highScoreViewModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"High Score with ID: {id}  was deleted");
            return Ok(highScoreViewModel);
        }

        private bool HighScoreViewModelExists(int id)
        {
            return _context.HighScoreViewModel.Any(e => e.ScoreId == id);
        }
    }
}