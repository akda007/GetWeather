using System.Text.Json;

namespace GetWeather{
    internal class Program {
        //You need a weatherapi.com account and a free api key in order to use this tool.
        const string API_KEY = "YOUR_API_KEY_HERE";

        public static string GetReqURL(string? location) {
            if(string.IsNullOrEmpty(API_KEY)) throw new Exception("Invalid weatherapi.com API!");
            if(string.IsNullOrEmpty(location)) throw new Exception(("Empty Location!"));

            return string.Format("http://api.weatherapi.com/v1/current.json?aqi=no&key={0}&q={1}", API_KEY, location);
        }

        class JsonFormatter {
            public TempStruct? Current { get; set; }

            public class TempStruct {
                public float temp_c { get; set; }
            }
        }

        static void Main(string[] args) {

            Console.Write("Location> ");
            string? loc = Console.ReadLine();

            string res;
            using(HttpClient client = new HttpClient()) {
                res = client.GetStringAsync(GetReqURL(loc)).Result;
            }

            var json = JsonSerializer.Deserialize<JsonFormatter>(res, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
            if (json?.Current != null) {
                Console.WriteLine("Temperature in {0}> {1:f1}° C", loc, json.Current.temp_c);
            }

        }
    }
}