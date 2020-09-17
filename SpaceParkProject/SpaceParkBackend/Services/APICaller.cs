using Newtonsoft.Json;
using RestSharp;
using SpaceParkBackend.Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SpaceParkBackend.Services
{
    public class APICaller
    {

        public string StarshipURL { get; set; }

        private static readonly RestClient client = new RestClient("https://swapi.dev/api/");

        public async Task<IRestResponse> GetPersonData(string name)
        {
            var request = new RestRequest("people/?search=" + name, DataFormat.Json);
            var response = client.ExecuteAsync<Person>(request);

            return await response;
        }

        public Person GetPerson(string name)
        {
            var dataResponse = GetPersonData(name);
            var data = JsonConvert.DeserializeObject<SwapiPersonResponse>(dataResponse.Result.Content);

            if (data.Results.Count == 0)
            {
                return null;
            }

            else if (data.Results[0].Name == name)
            {
                return data.Results[0];
            }

            else
            {
                return null;
            }
        }

        public async Task<IRestResponse> GetSpaceshipData(string URL)
        {
            var request = new RestRequest(URL, DataFormat.Json);
            var response = client.ExecuteAsync<Spaceship>(request);

            return await response;
        }
        public Spaceship GetStarship(string starShipURL)
        {
            Spaceship spaceship = new Spaceship();
            var response = GetSpaceshipData(starShipURL);
            var data = JsonConvert.DeserializeObject<SwapiSpaceshipResponse>(response.Result.Content);

            spaceship.Name = data.Name;
            spaceship.Length = Convert.ToDecimal(data.Length, CultureInfo.InvariantCulture);

            return spaceship;
        }
    }
}
