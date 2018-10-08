using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerProject.network
{
    class NetworkSyncer
    {
        private static readonly HttpClient client = new HttpClient();
        public static string getSHA1(string path)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var input = File.ReadAllBytes(path);
                var hash = sha1.ComputeHash(input);
                StringBuilder sb = new StringBuilder();
                foreach( var b in hash)
                {
                    sb.Append(b.ToString());
                }
                return sb.ToString();
            }
        }
        public static async Task<string> getTagsFromServer(string path, string url)
        {
            string sha1 = getSHA1(path);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+ "/check.php?sha1="+sha1);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        public static async Task<bool> uploadTagsToServer(string path, List<string> tags, string url)
        {
            FileInfo info = new FileInfo(path);
            var values = new Dictionary<string, string>
            {
                { "user", "paindar" },
                { "name", info.Name },
                { "sha1", getSHA1(path) },
                {"tags", string.Join(" ", tags) }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url + "/upload.php", content);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
