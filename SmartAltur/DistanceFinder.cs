using Newtonsoft.Json;
using SmartAltur.DTO;
using SmartAltur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartAltur
{
    public static class DistanceFinder
    {
        public static async Task<List<Distance>> GetAll(GeoLoc origin, List<IPassenger> passengers)
        {
            var result = new List<Distance>();

            string url = GetRequestUrl(origin, passengers.Select(p => p.Destination).ToList());

            var response = await GetDistanceResponse(url);

            for (int i = 0; i < passengers.Count; i++)
            {
                var distance = new Distance {
                    From = Vehicle.ORIGIN,
                    To = passengers[i].ID,
                    Meters = response.Rows[0].Elements[i].Distance.Value
                };

                result.Add(distance);
            }

            for (int i = 0; i < passengers.Count; i++)
            {
                var passenger = passengers[i];

                var otherPassengers = passengers.Where(p => !p.Equals(passenger)).ToList();

                string url2 = GetRequestUrl(passenger.Destination, otherPassengers.Select(p => p.Destination).ToList());

                var response2 = await GetDistanceResponse(url2);

                for (int j = 0; j < otherPassengers.Count; j++)
                {
                    var distance = new Distance
                    {
                        From = passenger.ID,
                        To = otherPassengers[j].ID,
                        Meters = response2.Rows[0].Elements[j].Distance.Value
                    };

                    result.Add(distance);
                }
            }

            return result;
        }

        private static string GetRequestUrl(GeoLoc origin, List<GeoLoc> destinations)
        {
            const string API_URL = "https://maps.googleapis.com/maps/api/distancematrix/json?";
            //const string API_KEY = "AIzaSyBhbo2sRdTJVEJ47Q4GTq4tMFslExcFOOs";
            const string API_KEY = "AIzaSyA2VMVwB8SO-vEt60C7-aSZj6V4DQdrMoI";

            string destinationsParam = $"{destinations[0].Latitude},{destinations[0].Longitude}";

            for (int i = 1; i < destinations.Count; i++)
            {
                destinationsParam += $"|{destinations[i].Latitude},{destinations[i].Longitude}";
            }

            string getReqUrl = $"{API_URL}origins={origin.Latitude},{origin.Longitude}&destinations={destinationsParam}&key={API_KEY}";

            return getReqUrl;
        }

        private static async Task<DistanceResponse> GetDistanceResponse(string getRequestUrl)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(getRequestUrl);

                HttpResponseMessage response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("GoogleDistanceMatrixApi failed with status code: " + response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DistanceResponse>(content);
                }
            }
        }
    }
}
