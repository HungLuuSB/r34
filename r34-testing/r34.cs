using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace r34_testing
{
    public enum SearchType
    {
        RANDOM,
        NEWEST,
        OLDEST,
        ALL
    }
    class r34
    {
        public r34()
        {
        }

        public Post[]? GetPost()
        {
            return null;
        }

        
        public List<Post> Search(List<string>? tags, int limit = 1000)
        {
            List<Post> result = new List<Post>();
            StringBuilder tags_format = new StringBuilder();
            foreach (var tag in tags)
            {
                if (tags.Last() != tag)
                    tags_format.Append(tag + "+");
                else
                    tags_format.Append(tag);
            }
            StringBuilder search_url = new StringBuilder($"https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&limit={limit}&tags={tags_format}&json=1");
            Console.WriteLine(search_url.ToString());
            var url = search_url.ToString();
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)HttpWReq.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        string json = responseReader.ReadToEnd();
                        var data = JArray.Parse(json);
                        foreach (var crawl_post in data)
                        {
                            
                            var image_url = crawl_post["file_url"].ToString();
                            string owner = crawl_post["owner"].ToString();
                            //Tags
                            List<string> post_tags = new List<string>();
                            foreach(var item in crawl_post["tags"].ToString().Split())
                            {
                                post_tags.Add(item.Trim());
                            }
                            string file_name = crawl_post["image"].ToString();
                            Post newPost = new Post(crawl_post["id"].ToString(), post_tags, owner, crawl_post["source"].ToString(), image_url, file_name);
                            result.Add(newPost);
                        }
                    }
                }
            }
            /*
            switch (SearchType)

            {
                case SearchType.RANDOM:
                    
                                Console.WriteLine("Choose random...");

                                Random rnd = new Random();
                                int r = rnd.Next(posts.Count);
                                string chosen_url = posts.Keys.ElementAt(r);
                                string chosen_image = posts.Values.ElementAt(r);
                                Console.WriteLine(chosen_url);
                                using (WebClient client = new WebClient())
                                {
                                    string file_name = @"E:\VSC Projects\r34-testing\r34-testing\Resources\Images\" + chosen_image;
                                    client.DownloadFile(new Uri(chosen_url), file_name);
                                }
                    break;
            }
            */
            return result;
        }
    }
}
