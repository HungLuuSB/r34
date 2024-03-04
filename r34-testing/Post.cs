using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace r34_testing
{
    class Post
    {
        public Post(string id, List<string> tags, string owner, string source, string url, string filename) 
        {
            Id = id;
            Tags = tags;
            Owner = owner;
            Source = source;
            Url = url;
            Filename = filename;
        }

        public string Id { get; }
        public List<string> Tags { get; }
        public string Owner { get; }
        public string Source { get; }
        public string Url { get; }
        public string Filename { get; }

        public void Download(string Download_path = "")
        {
            using (WebClient client = new WebClient())
            {
                string file_name = Download_path + Filename;
                client.DownloadFile(new Uri(Url), file_name);
            }
            /*
            if (string.IsNullOrEmpty(filename))
            {
                using (WebClient client = new WebClient())
                {
                    string file_name = @"E:\VSC Projects\r34-testing\r34-testing\Resources\Images\" + Filename;
                    client.DownloadFile(new Uri(Url), file_name);
                }
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    string file_name = @"E:\VSC Projects\r34-testing\r34-testing\Resources\Images\" + filename;
                    client.DownloadFile(new Uri(Url), file_name);
                }
            }
            */
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append($"ID:{Id}\n");
            stringBuilder.Append($"Tags:[");
            foreach (var tag in Tags)
            {
                if (Tags.Last() != tag)
                    stringBuilder.Append(tag + ",");
                else
                    stringBuilder.Append(tag);
            }
            stringBuilder.Append($"]\n");
            stringBuilder.Append($"Url:{Url}\n");
            stringBuilder.Append($"Filename:{Filename}\n");
            return stringBuilder.ToString();
        }
    }
}
