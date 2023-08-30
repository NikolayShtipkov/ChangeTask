using System.Net;
using System.Xml;

namespace ChangeTasks
{
    class Program
    {
        static void Main()
        {
            string feedUrl = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";

            WebRequest request = WebRequest.Create(feedUrl);

            // Get the response from the WebRequest
            using (WebResponse response = request.GetResponse())
            {
                // Open the response stream
                using (Stream stream = response.GetResponseStream())
                {
                    // Create an XmlReader to read the XML data
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        // Read the XML data
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.Name);
                        }
                    }
                }
            }
        }
    }
}