using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;

namespace LUIS
{
    class Program
    {
        static void Main(string[] args)
        {
            string appId = "APPID";
            string endpoint = "ENDPOINT";
            string key = "KEY";
            Console.WriteLine("Input pesan: ");
            string msg = Console.ReadLine();
            var client = Authenticate(endpoint, key);
            string intent = GetLuisIntent(client, msg, appId);
            Console.WriteLine($"Intent yang dikenali {intent}");
            Console.ReadLine();
        }

        private static LUISRuntimeClient Authenticate(string endpoint, string key)
        {
            var credentials = new ApiKeyServiceClientCredentials(key);
            var client = new LUISRuntimeClient(credentials);
            client.Endpoint = endpoint;
            return client;
        }

        private static string GetLuisIntent(LUISRuntimeClient client, string teks, string appId)
        {
            var prediction = new Prediction(client);
            var predicitionResult = prediction.ResolveAsync(appId, teks,null,false,true,false,null,false).Result;
            return predicitionResult.TopScoringIntent.Intent;
        }
    }
}
