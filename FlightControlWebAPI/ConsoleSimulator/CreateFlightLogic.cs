using DtoModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSimulator
{
    public static class CreateFlightLogic
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5127") };

        //var flights = await client.GetFromJsonAsync<IEnumerable<FlightDto>>("api/Flights");
        static public void CreateFlight()
        {
            Random rnd = new Random();
            var flight = new FlightDto
            {
                Brand = (BrandTypeDto)rnd.Next(0, 6),
                FlightName = $"{rnd.Next(0,100)}-{RandomLetterGenerator()}",
                //FlightName = ((BrandTypeDto)rnd.Next(0, 6)).ToString().Substring(0, 4) + RandomLetterGenerator(),
                //FlightName = string.Concat(((BrandTypeDto)rnd.Next(0, 6)).ToString().AsSpan(0, 4), RandomLetterGenerator()),
                SerialNumber = Guid.NewGuid().ToString()
            };
            client.PostAsJsonAsync("api/Flights", flight);
            PrintFlight(flight);
        }

        static void PrintFlight(FlightDto flight) =>
            Console.WriteLine($"{flight.Brand} - {flight.FlightName} - {flight.SerialNumber}");

        static string RandomLetterGenerator()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8).
                Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
