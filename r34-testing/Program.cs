using System;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Collections.Specialized;
namespace r34_testing
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\";
            if (ConfigurationManager.AppSettings.Get("prefered_path") != "n/a")
            {
                if (Directory.Exists(ConfigurationManager.AppSettings.Get("prefered_path")))
                {
                    path = ConfigurationManager.AppSettings.Get("prefered_path");
                }
            }
            else
            {
                Console.WriteLine($"Leave blank to use default path\nDefault path:{path}\nInput path to save image(s): ");
                string new_path = Console.ReadLine();
                if (!string.IsNullOrEmpty(new_path))
                {
                    if (Directory.Exists(new_path))
                    {
                        path = new_path;
                        ConfigurationManager.AppSettings.Set("prefered_path", new_path);
                    }
                }
            }
            int limit;
            Console.WriteLine("Input tags(lower case, seperated by ','): ");
            string[] tags = Console.ReadLine().Trim().ToLower().Split(',');
            List<string> tags_list = new List<string>();
            foreach (var tag in tags)
                tags_list.Add(tag);
            Console.WriteLine("Input limit (0<limit<1000): ");
            limit = Convert.ToInt32(Console.ReadLine());
            r34 testing = new r34();
            List<Post> output = testing.Search(tags_list,limit);
            foreach(Post post in output)
            {
                Console.WriteLine(post.ToString());
                post.Download(path);
            }
        }
    }
}
