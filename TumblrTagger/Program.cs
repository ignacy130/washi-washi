using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var factory = new TumblrClientFactory();
            var client = factory.Create<TumblrClient>(Credentials.Key, Credentials.Secret);
            var posts = new List<BasePost>();
            for (int i = 0; i < 100; i+=20)
            {
                var next = await client.GetPostsAsync("malowiele", 0, 20);
                posts.AddRange(next.Result.ToList());
            }
        }
    }
}
