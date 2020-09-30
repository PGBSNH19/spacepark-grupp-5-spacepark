using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Models;
using SpaceParkBackend.Repos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceParkBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StarshipController : ControllerBase
    {
        private readonly IStarshipRepository _starshipRepository;
        private readonly ILogger _logger;

        public StarshipController(IStarshipRepository starshipRepository, ILogger<PersonController> logger)
        {
            _starshipRepository = starshipRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Starship>>> GetStarships()
        {
            var results = await _starshipRepository.GetAllStarships();
            try
            {
                if (results.Count > 0 || results != null)
                {
                    _logger.LogInformation("Getting starships from database");
                    return Ok(results);
                }
                else
                {
                    _logger.LogInformation($"Could not get any starships from the database");
                    return NotFound();
                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"Could not get any starships from the database");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {exception.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Starship>> GetStarship(int id)
        {
            var result = await _starshipRepository.GetStarshipById(id);
            try
            {
                if (result != null)
                {
                    _logger.LogInformation($"Getting starship with id {id}.");
                    return result;
                }
                else
                {
                    return NotFound();

                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"Could not get starship with id {id} from the database");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {exception.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Starship>> PostStarship(Starship starship)
        {
            try
            {
                await _starshipRepository.Add(starship);

                if(await _starshipRepository.Save())
                {
                    _logger.LogInformation($"Creating starship and posting in database");
                    return Created($"/Starship/{starship.StarshipID}", new Starship { StarshipID = starship.StarshipID, Name = starship.Name, Length = starship.Length });
                }

                return BadRequest();
          
            }

            catch (Exception exception)
            {
                _logger.LogInformation("Something went wrong while posting a person to the database");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Starship>> UpdateStarship(int id, Starship starship)
        {
            try
            {
                var starshipFromRepo = await _starshipRepository.GetStarshipById(id);

                if (starshipFromRepo != null)
                {
                    starshipFromRepo.ParkinglotID = starship.ParkinglotID;

                    _starshipRepository.Update(starshipFromRepo);
                    await _starshipRepository.Save();
                }

                else
                {
                    _logger.LogInformation($"Could not update Starship. Starship with id {id} was not found.");
                    return NotFound($"Could not update Person. Person with id {id} was not found.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Something went wrong while uptading person with id {starship.StarshipID} in the database");
                var result = new { Status = StatusCodes.Status500InternalServerError, Data = $"Failed to update the person. Exception thrown when attempting to update data in the database: {e.Message}" };
                return this.StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStarship(int id)
        {
            try
            {
                var existingStarship = await _starshipRepository.GetStarshipById(id);
                if (existingStarship == null)
                {
                    return NotFound($"Could not find a starship with id {id}");
                }

                _starshipRepository.Delete(existingStarship);
                if (await _starshipRepository.Save())
                {
                    _logger.LogInformation($"Deleting {existingStarship} from the database.");
                    return NoContent();
                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"Something went wrong while deleting the starship from the database");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
            return BadRequest();
        }
    }
}
