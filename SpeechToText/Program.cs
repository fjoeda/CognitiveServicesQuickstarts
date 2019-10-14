using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechToText
{
    class Program
    {
        static void Main(string[] args)
        {
            string region = "southeastasia";
            string key = "key";
            RecognizeSpeechAsync(region,key).Wait();
            Console.ReadLine();
        }

        public static async Task RecognizeSpeechAsync(string region, string key)
        {
            var config = SpeechConfig.FromSubscription(key, region);

            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Katakan sesuatu...");
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"Terdeteksi: {result.Text}");
                }
            }
        }
    }
}
