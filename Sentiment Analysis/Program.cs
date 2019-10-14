using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sentiment_Analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input text :");
            string text = Console.ReadLine();
            string endpoint = "https://resource-name.cognitiveservices.azure.com/";
            string apiKey = "resource key";
            var client = Authenticate(endpoint, apiKey);
            var result = GetSentiment(client, text);
            Console.WriteLine($"Sentiment Score : {result}");
            Console.ReadLine();
        }

        private static TextAnalyticsClient Authenticate(string endpoint, string key)
        {
            var credentials = new ApiKeyServiceClientCredentials(key);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };
            return client;
        }

        private static double GetSentiment(TextAnalyticsClient client,string text)
        {
            return (double)client.Sentiment(text).Score;
        }
    }

    class ApiKeyServiceClientCredentials : ServiceClientCredentials
    {
        private readonly string apiKey;

        public ApiKeyServiceClientCredentials(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            request.Headers.Add("Ocp-Apim-Subscription-Key", this.apiKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
