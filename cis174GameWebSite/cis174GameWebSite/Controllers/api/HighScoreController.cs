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

        // GET: api/HighScore
        [HttpGet]
        public IEnumerable<HighScoreViewModel> GetHighScoreViewModel()
        {
            return _context.HighScoreViewModel;
        }

        //GET: api/HighScore/5
        [HttpGet]
        [Route("getscores")]
        public async Task<IActionResult> GetHighScoresForUser([FromBody] HighScoreViewModel highScore)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state invalid");
                return BadRequest(ModelState);
            }

            //var highScoreViewModel = await _context.HighScoreViewModel.FindAsync(id);

            var highScores = _context.HighScoreViewModel
                                .Where(a => a.UserId == highScore.UserId)
                                .Select(a => new HighScoreViewModel
                                {
                                    ScoreId = a.ScoreId,
                                    Score = a.Score,
                                    UserId = a.UserId,
                                });

            if (highScores == null)
            {
                _logger.LogWarning($"Highscores not found {highScore.UserId}");
                return NotFound();
            }

            _logger.LogInformation($"High Scores retrieved for {highScore.UserId}");
            return Ok(highScores);
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