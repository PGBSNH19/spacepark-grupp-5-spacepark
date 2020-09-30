using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAllPersons")]
        public async Task<IList<Person>> GetAllPersons()
        {
            var results = await _repository.GetAllPersons();

            return results;
        }

        [HttpGet("{id}")]
        public async Task<Person> GetPerson(int id)
        {
            var result = await _repository.GetPersonById(id);

            return result;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Person>> ValidatePerson(string name)
        {
            var personsFromRepo = await _repository.GetAllPersons();
            foreach (var p in personsFromRepo)
            {
                if (p.Name == name)
                    return Ok(p);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            try
            {
                var personsFromRepo = await _repository.GetAllPersons();
                foreach (var p in personsFromRepo)
                {
                    if (p.Name == person.Name)
                        return Unauthorized($"You are already parked here, {person.Name}!");
                }

                var validatedPerson = APICaller.GetPerson(person.Name);
                var validatedStarship = new Starship();

                if (validatedPerson != null)
                {
                    if (validatedPerson.Starships.Count != 0)
                    {
                        validatedStarship = APICaller.GetStarship(validatedPerson.Starships[0]);
                    }

                    else
                    {
                        validatedStarship = APICaller.GetStarship("https://swapi.dev/api/starships/12/");
                    }

                    validatedPerson.Starship = validatedStarship;
                    await _repository.Add(validatedPerson);

                    if (await _repository.Save())
                    {
                        return Ok(validatedPerson);
                    }

                    return BadRequest();

                }

                else
                {
                    return BadRequest();
                }

            }

            catch (Exception exception)
            {
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
                    return NotFound($"Could not update Person. Person with id {id} was not found.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                var result = new { Status = StatusCodes.Status500InternalServerError, Data = $"Failed to update the person. Exception thrown when attempting to update data in the database: {e.Message}" };
                return this.StatusCode(StatusCodes.Status500InternalServerError, result);
            }                     
        }

        [HttpDelete(Name = "DeletePerson")]
        public async Task<ActionResult> DeletePerson(Person person)
        {
            try
            {
                var personsFromRepo = await _repository.GetAllPersons();
                foreach (var p in personsFromRepo)
                {
                    if (p.Name == person.Name)
                    {
                        _repository.Delete(p);
                        await _repository.Save();

                        return NoContent();
                    }
                }
                return NotFound($"You are not parked here, {person.Name}!");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Databases Failure: {e.Message}");
            }
        }
    }
}
