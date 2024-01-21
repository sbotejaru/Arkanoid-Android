using Microsoft.AspNetCore.Mvc;
using Core.Services;
using DataLayer.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/scores")]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService scoreService = new ScoreService();

        [HttpGet("/get-all-scores")]
        public ActionResult<List<Score>> GetAll()
        {
            var result = scoreService.GetAll();

            return Ok(result);
        }

        [HttpPatch("/update-score")]
        public ActionResult<bool> UpdateScoreName([FromBody] Score scoreUpdateModel)
        {
            var result = scoreService.Update(scoreUpdateModel);

            if (!result)
                return BadRequest("Score could not be updated.");

            return Ok(result);
        }

        [HttpPost("/add-score")]
        public IActionResult Add([FromBody] Score payload)
        {
            var result = scoreService.Add(payload);

            if (!result)
                return BadRequest("Score cannot be added");

            return Ok(result);
        }

        [HttpGet("/get-score-by-id/{id}")]
        public ActionResult<Score> GetByUserId(int id)
        {
            var result = scoreService.GetById(id);

            if (result == null)
                return BadRequest("Score cannot be found");

            return Ok(result);
        }

        [HttpPatch("/delete-score/{id}")]
        public ActionResult<bool> DeleteScore(int id)
        {
            var result = scoreService.Delete(id);

            if (!result)
                return BadRequest("Score cannot be found");

            return Ok(result);
        }

        [HttpGet("/get-highscore-by-username/{username}")]
        public ActionResult<Score> GetHighscoreByUsername(string username)
        {
            var result = scoreService.GetHighscoreByUsername(username);

            if (result == null)
                return BadRequest("Score cannot be found");

            return Ok(result);
        }

        [HttpGet("/get-highest-score")]
        public ActionResult<Score> GetHighestScore()
        {
            var result = scoreService.GetHighestScore();

            if (result == null)
                return BadRequest("Score cannot be found");

            return Ok(result);
        }
    }
}
