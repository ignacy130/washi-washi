using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TumblrTagger
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var path = @"C:\D\Projekty\Aplikacje\ML\TumblrTagger.Data\posts.json";
            //var posts = await DownloadPostsData();
            //new JsonSaver().Save(posts, path);
            var postsRead = JsonConvert.DeserializeObject<List<PhotoPost>>(File.ReadAllText(path));
            var paintings = postsRead.Where(p => p.Tags.Contains("painting")).Count();
            var photos = postsRead.Where(p => p.Tags.Contains("photo") || p.Tags.Contains("photography")).Count();
            var graphics = postsRead.Where(p => p.Tags.Contains("graphics")).Count();
            var drawings = postsRead.Where(p => p.Tags.Contains("drawing")).Count();
            Console.WriteLine($"paintings: {paintings}");
            Console.WriteLine($"photos: {photos}");
            Console.WriteLine($"graphics: {graphics}");
            Console.WriteLine($"drawings: {drawings}");
            //DownloadImages(postsRead);
        }

        private static void DownloadImages(List<PhotoPost> postsRead)
        {
            var webClient = new WebClient();
            try
            {
                var i = 0;
                foreach (var post in postsRead)
                {
                    webClient.DownloadFile((post).Photo.OriginalSize.ImageUrl, @"C:\D\Projekty\Aplikacje\ML\TumblrTagger.Data\images\" + post.Id + ".jpg");
                    Console.WriteLine(i++);
                    Thread.Sleep(250);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<List<BasePost>> DownloadPostsData()
        {
            var factory = new TumblrClientFactory();
            var client = factory.Create<TumblrClient>(Credentials.Key, Credentials.Secret);
            var posts = new List<BasePost>();
            for (int i = 0; i < 1000; i += 20)
            {
                var next = await client.GetPostsAsync("malowiele", i, 20, PostType.Photo);
                posts.AddRange(next.Result.ToList());
            }

            return posts;
        }
    }
}
