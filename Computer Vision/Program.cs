using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Vision
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageUrl = "http://i.dailymail.co.uk/i/pix/2017/04/05/08/3EF0483400000578-4379890-NOT_FOR_USE_ON_GREETING_CARDS_POSTCARDS_CALENDARS_OR_ANY_MERCHAN-a-79_1491379088606.jpg";
            var client = Authenticate("https://resource-name.cognitiveservices.azure.com/", "key");
            AnalyzeImageUrl(client, imageUrl);
            Console.ReadLine();
        }

        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client =
                new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
                { Endpoint = endpoint };
            return client;
        }


        public static void AnalyzeImageUrl(ComputerVisionClient client, string imageUrl)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("ANALYZE IMAGE - URL");
            Console.WriteLine();

            List<VisualFeatureTypes> features = new List<VisualFeatureTypes>()
                {
                    VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects
                };

            ImageAnalysis results = client.AnalyzeImageAsync(imageUrl, features).Result;

            // Menampilkan Ringkasan Gambar
            Console.WriteLine("Summary:");
            foreach (var caption in results.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with confidence {caption.Confidence}");
            }
            Console.WriteLine();

            // Menampilkan Kategori Gambar
            Console.WriteLine("Categories:");
            foreach (var category in results.Categories)
            {
                Console.WriteLine($"{category.Name} with confidence {category.Score}");
            }
            Console.WriteLine();

            // Menampilkan Tag dari gambar
            Console.WriteLine("Tags:");
            foreach (var tag in results.Tags)
            {
                Console.WriteLine($"{tag.Name} {tag.Confidence}");
            }
            Console.WriteLine();

        }
    }
}
