using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.IO;


namespace Content_Moderator
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "This crap is useless as hell";
            var client = Authenticate("ENDPOINT", "KEY");
            AnalyzeContent(client, text);
            Console.ReadLine();
        }

        private static ContentModeratorClient Authenticate(string endpoint, string key)
        {
            // Create and initialize an instance of the Content Moderator API wrapper.
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(key));

            client.Endpoint = endpoint;
            return client;
        }

        private static void AnalyzeContent(ContentModeratorClient client,string text)
        {
            Console.WriteLine($"Menganalisa Teks : {text}...");
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(text);
            MemoryStream stream = new MemoryStream(byteArray);
            var result = client.TextModeration.ScreenText("text/plain", stream, "eng", true, true, null, true);
            Console.WriteLine($"Kategori 1 : {result.Classification.Category1.Score}");
            Console.WriteLine($"Kategori 2 : {result.Classification.Category2.Score}");
            Console.WriteLine($"Kategori 3 : {result.Classification.Category3.Score}");
            if ((bool)result.Classification.ReviewRecommended)
                Console.WriteLine("Teks mengandung konten negatif");
            else
                Console.WriteLine("Teks tidak mengandung konten negatif");
        }
    }
}
