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

        [HttpPost(Name = "AddPerson")]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            try
            {
                var validatedPerson = APICaller.GetPerson(person.Name);
                var validatedStarship = new Starship();                                           

                if (validatedPerson != null)
                {
                    if (validatedPerson.Starships != null)
                    {
                        validatedStarship = APICaller.GetStarship(validatedPerson.Starships[0]);
                    }

                    await _repository.Add(validatedPerson);

                    if (await _repository.Save())
                    {
                        return Created($"/Person/{validatedPerson.PersonID }", new Person { Name = validatedPerson.Name, HasPaid = false, StarshipID = validatedStarship.StarshipID });
                    }

                    return BadRequest();
                }

                else
                {
                    return NoContent();
                }
            }

            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, Person person)
        {
            try
            {
                var personFromRepo = await _repository.GetPersonById(id);

                if (personFromRepo == null)
                {
                    return NotFound($"Could not update Person. Person with id {id} was not found.");
                }

                var personToUpdate = new Person { PersonID = person.PersonID, HasPaid = person.HasPaid };
                _repository.Update(personToUpdate);

                return NoContent();
            }
            catch (Exception e)
            {
                var result = new { Status = StatusCodes.Status500InternalServerError, Data = $"Failed to update the person. Exception thrown when attempting to update data in the database: {e.Message}" };
                return this.StatusCode(StatusCodes.Status500InternalServerError, result);
            }                     
        }

        [HttpDelete("{id}", Name = "DeletePerson")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                var visitorToDelete = await _repository.GetPersonById(id);

                if(visitorToDelete == null)
                {
                    return NotFound($"Could not find a person with the id {id}");
                }

                _repository.Delete(visitorToDelete);

                if(await _repository.Save())
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Databases Failure: {e.Message}");
            }

            return BadRequest();
        }
    }
}
