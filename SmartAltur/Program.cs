using Newtonsoft.Json;
using SmartAltur.DTO;
using System.IO;


// Google Maps Distance Matrix API
// AIzaSyBhbo2sRdTJVEJ47Q4GTq4tMFslExcFOOs

namespace SmartAltur
{
    public class Program
    {
        static void Main(string[] args)
        {
            string inputJson = @"ten_workers_from_kollektif.json";

            if (args.Length > 0)
            {
                inputJson = args[0];
            }

            TakeThemHome data  = JsonConvert.DeserializeObject<TakeThemHome>(File.ReadAllText(inputJson));
        }
    }
}
