using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/28/Child%27s_Angry_Face.jpg/1200px-Child%27s_Angry_Face.jpg";
            var client = Authenticate("https://resource-name.cognitiveservices.azure.com/", "key");
            DetectFaceExtract(client, imageUrl, RecognitionModel.Recognition01);
            Console.ReadLine();
        }

        public static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        public static void DetectFaceExtract(IFaceClient client, string url, string recognitionModel)
        {
            Console.WriteLine("========Deteksi Wajah========");

            IList<DetectedFace> detectedFaces;

            // Detect faces with all attributes from image url.
            detectedFaces = client.Face.DetectWithUrlAsync($"{url}",
                    returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age,
                FaceAttributeType.Blur, FaceAttributeType.Emotion, FaceAttributeType.Exposure, FaceAttributeType.FacialHair,
                FaceAttributeType.Gender, FaceAttributeType.Glasses, FaceAttributeType.Hair, FaceAttributeType.HeadPose,
                FaceAttributeType.Makeup, FaceAttributeType.Noise, FaceAttributeType.Occlusion, FaceAttributeType.Smile },
                    recognitionModel: recognitionModel).Result;

            Console.WriteLine($"{detectedFaces.Count} Wajah terdeteksi.");
            foreach (var face in detectedFaces)
            {
                Console.WriteLine($"Detail Wajah ID :{face.FaceId}");
                Console.WriteLine($"Ekspresi Senang {face.FaceAttributes.Emotion.Happiness}");
                Console.WriteLine($"Ekspresi Sedih {face.FaceAttributes.Emotion.Sadness}");
                Console.WriteLine($"Ekspresi Marah {face.FaceAttributes.Emotion.Anger}");
                Console.WriteLine($"Ekspresi Jijik {face.FaceAttributes.Emotion.Disgust}");
                Console.WriteLine($"Ekspresi Netral {face.FaceAttributes.Emotion.Neutral}");
                Console.WriteLine($"Ekspresi Takut {face.FaceAttributes.Emotion.Fear}");
                Console.WriteLine($"Umur {face.FaceAttributes.Age}");
                Console.WriteLine($"Gender {face.FaceAttributes.Gender}");
            }
            
        }
    }
}
