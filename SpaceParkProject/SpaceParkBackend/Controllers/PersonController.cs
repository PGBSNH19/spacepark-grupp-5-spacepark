using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceParkBackend.Models;
using SpaceParkBackend.Repos;
using SpaceParkBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpaceParkBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger _logger;

        public PersonController(IPersonRepository repository, ILogger<PersonController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPersons")]
        public async Task<IList<Person>> GetAllPersons([FromQuery]string name)
        {
            var results = await _repository.GetAllPersons(name);
            _logger.LogInformation("Getting all persons from database");

            return results;
        }

        [HttpGet("{id}")]
        public async Task<Person> GetPerson(int id)
        {
            var result = await _repository.GetPersonById(id);
            _logger.LogInformation($"Getting person from database with id {id}");

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            try
            {
                var personsFromRepo = await _repository.GetAllPersons("");
                foreach (var p in personsFromRepo)
                {
                    if (p.Name == person.Name)
                    {
                        _logger.LogInformation($"A person with the name {person.Name}, already exists in the database. A person cannot be posted twice.");
                        return Unauthorized($"You are already parked here, {person.Name}!"); 
                    }                  
                }

                var validatedPerson = APICaller.GetPerson(person.Name);
                var validatedStarship = new Starship();

                if (validatedPerson != null)
                {
                    if (validatedPerson.Starships.Count != 0)
                    {
                        validatedStarship = APICaller.GetStarship(validatedPerson.Starships[0]);
                        _logger.LogInformation($"{validatedPerson.Name} got the starship {validatedStarship.Name}");
                    }

                    else
                    {
                        validatedStarship = APICaller.GetStarship("https://swapi.dev/api/starships/12/");
                        _logger.LogInformation($"The person who entered did not have a starship of their own. Default ship {validatedStarship.Name} was provided.");
                    }

                    validatedPerson.Starship = validatedStarship;
                    await _repository.Add(validatedPerson);

                    if (await _repository.Save())
                    {
                        _logger.LogInformation($"{validatedPerson.Name} was posted in the database.");
                        return Ok(validatedPerson);
                    }

                    return BadRequest();

                }

                else
                {
                    _logger.LogInformation($"The server cannot or will not process the request. This could be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing)");
                    return BadRequest();
                }

            }

            catch (Exception exception)
            {
                _logger.LogInformation($"Something went wrong while posting a person to the database");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, Person person)
        {
            try
            {
                var personFromRepo = await _repository.GetPersonById(id);

                if (personFromRepo != null)
                {
                    personFromRepo.Name = person.Name;
                    personFromRepo.PersonID = person.PersonID;
                    personFromRepo.HasPaid = person.HasPaid;
                    personFromRepo.Starship = person.Starship;

                    _repository.Update(personFromRepo);
                    await _repository.Save();
                }

                else
                {
                    _logger.LogInformation($"Could not update Person. Person with id {id} was not found.");
                    return NotFound($"Could not update Person. Person with id {id} was not found.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Something went wrong while uptading person with id {person.PersonID} in the database");
                var result = new { Status = StatusCodes.Status500InternalServerError, Data = $"Failed to update the person. Exception thrown when attempting to update data in the database: {e.Message}" };
                return this.StatusCode(StatusCodes.Status500InternalServerError, result);
            }                     
        }

        [HttpDelete(Name = "DeletePerson")]
        public async Task<ActionResult> DeletePerson(Person person)
        {
            try
            {
                var personsFromRepo = await _repository.GetAllPersons("");
                foreach (var p in personsFromRepo)
                {
                    if (p.Name == person.Name)
                    {
                        _repository.Delete(p);
                        await _repository.Save();

                        _logger.LogInformation($"Deleting {person.Name} from the database.");
                        return NoContent();
                    }
                }
                return NotFound($"You are not parked here, {person.Name}!");
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Something went wrong while deleting person with id {person.PersonID} in the database");
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Databases Failure: {e.Message}");
            }
        }
    }
}
