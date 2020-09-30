using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Models;
using SpaceParkBackend.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParkinglotController : ControllerBase
    {
        private readonly IParkinglotRepo _parkinglotRepo;
        private readonly ILogger _logger;
        
        public ParkinglotController(IParkinglotRepo parkinglotRepo, ILogger<ParkinglotController> logger)
        {
            _parkinglotRepo = parkinglotRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Parkinglot>> GetAllParkinglots()
        {
            try
            {
                var results = await _parkinglotRepo.GetAvailableParkingLots();

                if (!results.Any())
                {
                    _logger.LogInformation("Cannot get any parkinglots from the database");
                    return NotFound("Its Full B*TCH (Shoot em Down)");
                }


                else
                {
                    _logger.LogInformation("Getting parkinglots from the database");
                    return Ok(results);
                }
                    
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Parkinglot>> GetParkingLotByID(int id)
        {
            try
            {
                _logger.LogInformation($"Getting parkinglot with id {id}");
                var result = await _parkinglotRepo.GetParkinglotById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPut("{parkinglotID}")]
        public async Task<ActionResult<Parkinglot>> UpdateParkinglots(int parkinglotID, Parkinglot parkinglot)
        {
            try
            {
                var existingParkinglot = await _parkinglotRepo.GetParkinglotById(parkinglotID);
               
                if(existingParkinglot != null)
                {
                    if (parkinglot.Starship != null)
                    {
                        if (existingParkinglot.Length > parkinglot.Starship.Length)
                        {
                            existingParkinglot.IsOccupied = parkinglot.IsOccupied;
                            existingParkinglot.Starship = parkinglot.Starship;

                            _parkinglotRepo.Update(existingParkinglot);
                            await _parkinglotRepo.Save();
                        }
                        else
                        {
                            return BadRequest("Whoops! Your starship was too big for the parkinglot!");
                        }
                    }

                    else
                    {
                        existingParkinglot.IsOccupied = parkinglot.IsOccupied;
                        existingParkinglot.Starship = null;

                        _parkinglotRepo.Update(existingParkinglot);
                        await _parkinglotRepo.Save();
                    }
                }
                else
                    return NotFound($"There is no parkinglot with the ID : {parkinglotID}");

                _logger.LogInformation($"Updating parkinglot with id {existingParkinglot.ParkinglotID} in the database");
                return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
          
        }
    }
}