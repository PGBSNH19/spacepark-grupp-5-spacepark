using System;
using Xunit;
using SpaceParkBackend.Services;
using Newtonsoft.Json;
using SpaceParkBackend.Models;

namespace Spacepark.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetPerson_Test()
        {
            APICaller api = new APICaller();
            var person = api.GetPerson("Luke Skywalker");               

            Assert.Equal("Luke Skywalker", person.Name);
        }

        [Fact]
        public void GetSpaceShip_Test()
        {
            APICaller api = new APICaller();                
            var person = api.GetPerson("Luke Skywalker");         
            var spaceship = api.GetStarship(person.Starships[1]);
                       
            Assert.Equal("Imperial shuttle", spaceship.Name);
        }


    }
}
