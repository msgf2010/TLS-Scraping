using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace LakewoodScoopScraping
{
    public static class Api
    {
        public static List<Story> ScrapeTLS()
        {
            var html = GetTLSHtml();
            return GetStories(html);
        }

        private static string GetTLSHtml()
        {
            var handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("user-agent", "lakewood scoop");
                var url = $"https://www.thelakewoodscoop.com/";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }

        private static List<Story> GetStories(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            var storyDivs = document.QuerySelectorAll(".post");
            List<Story> stories = new List<Story>();
            foreach (var div in storyDivs)
            {
                Story story = new Story();
                var href = div.QuerySelectorAll("a").First();
                story.Title = href.TextContent.Trim();
                story.Url = href.Attributes["href"].Value;

                var image = div.QuerySelectorAll("img").First();
                if (image != null)
                {
                    story.ImageUrl = image.Attributes["src"].Value;
                }

                stories.Add(story);
            }

            return stories;
        }
    }
}