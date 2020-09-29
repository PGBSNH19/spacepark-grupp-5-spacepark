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

        public static async Task<IRestResponse> GetPersonData(string name)
        {
            var theName = Uri.EscapeUriString(name);
            var request = new RestRequest($"people/?search={theName}", DataFormat.Json);
            var response = client.ExecuteAsync<SwapiPersonResponse>(request);

            return await response;
        }       

        public static Person GetPerson(string name)
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

        public static async Task<IRestResponse> GetStarshipData(string URL)
        {
            var request = new RestRequest(URL, DataFormat.Json);
            var response = client.ExecuteAsync<SwapiSpaceshipResponse>(request);

            return await response;
        }
        public static Starship GetStarship(string starShipURL)
        {
            Starship starship = new Starship();
            var response = GetStarshipData(starShipURL);
            var data = JsonConvert.DeserializeObject<SwapiSpaceshipResponse>(response.Result.Content);

            starship.StarshipID = data.ID;
            string convert = data.Length;
            string toNumber = convert.Split('.')[0].Trim();           
            starship.Name = data.Name;
            starship.Length = Convert.ToInt32(toNumber);

            return starship;
        }
    }
}

