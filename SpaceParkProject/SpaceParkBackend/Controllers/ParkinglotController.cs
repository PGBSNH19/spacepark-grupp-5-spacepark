using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        
        public ParkinglotController(IParkinglotRepo parkinglotRepo)
        {
            _parkinglotRepo = parkinglotRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Parkinglot>> GetAllParkinglots()
        {
            try
            {
                var results = await _parkinglotRepo.GetAvailableParkingLots();
                return Ok(results);
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
                var result = await _parkinglotRepo.GetParkinglotById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
        }

        [HttpPut("{parkinglotID}")]
        public async Task<ActionResult<Parkinglot>> UpdateParkinglots(int parkinglotID, Starship starship)
        {
            try
            {
                var existingParkinglot = await _parkinglotRepo.GetParkinglotById(parkinglotID);
               
                if(existingParkinglot == null)
                {
                    return NotFound($"There is no parkinglot with the ID : {parkinglotID}");
                }

                existingParkinglot.IsOccupied = true;
                existingParkinglot.Starship = starship;
                existingParkinglot.StarshipID = starship.StarshipID;
                _parkinglotRepo.Update(existingParkinglot);
                

                if(await _parkinglotRepo.Save())
                {
                    return NoContent();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
          
        }
    }
}