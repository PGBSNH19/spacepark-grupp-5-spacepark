using System;
using Xunit;
using SpaceParkBackend.Services;

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
    }
}
