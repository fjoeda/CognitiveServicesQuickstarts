using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Vision
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageUrl = "https://media.thetab.com/blogs.dir/90/files/2019/09/nature-outdoors-human-person-940x480.jpeg";
            string publishedName = "Published name";
            var projectId = new Guid("project id");
            var client = Authenticate("https://project-name.cognitiveservices.azure.com/", "key");
            PerformPrediction(client, imageUrl,projectId, publishedName);
        }

        public static CustomVisionPredictionClient Authenticate(string endpoint, string key)
        {
            CustomVisionPredictionClient client =
                new CustomVisionPredictionClient()
                {
                    ApiKey = key,
                    Endpoint = endpoint 
                };
            return client;
        }

        public static void PerformPrediction(CustomVisionPredictionClient client,string imageUrl,
            Guid projectId,string publishedName)
        {
            var url = new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models.ImageUrl(imageUrl);
            Console.WriteLine("Making a prediction:");
            var result = client.ClassifyImageUrl(projectId, publishedName, url);

            // Loop over each prediction and write out the results
            foreach (var c in result.Predictions)
            {
                Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
            }
            Console.ReadKey();
        }
    }
}
