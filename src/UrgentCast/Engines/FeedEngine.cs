using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel.Syndication;
using UrgentCast.Data;
using UrgentCast.Models;

namespace UrgentCast.Engines
{
    public class FeedEngine
    { 
        public FeedEngine()
        {
            
        }

        public static SyndicationFeed GenerateFeed(Feed feed)
        {
            XNamespace itunesNS = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            string prefix = "itunes";

            var synFeed = new SyndicationFeed(feed.Title, feed.Description, new Uri(feed.Link));
            synFeed.Categories.Add(new SyndicationCategory(feed.Category));
            synFeed.AttributeExtensions.Add(new System.Xml.XmlQualifiedName(prefix,
                "http://www.w3.org/2000/xmlns/"), itunesNS.NamespaceName);
            synFeed.Copyright = new TextSyndicationContent("Copyright UrgentCast " + DateTime.UtcNow.AddHours(-3).Year.ToString());
            synFeed.Language = feed.Language;
            synFeed.ImageUrl = new Uri(feed.ImageURL);
            synFeed.LastUpdatedTime = DateTime.UtcNow.AddHours(-3);
            synFeed.Authors.Add(new SyndicationPerson() { Name = feed.Author, Email = feed.Email });

            var extensions = synFeed.ElementExtensions;

            extensions.Add(new XElement(itunesNS + "subtitle", "This is the subtitle").CreateReader()); // TODO
            extensions.Add(new XElement(itunesNS + "image", new XAttribute("href", feed.ImageURL)).CreateReader());
            extensions.Add(new XElement(itunesNS + "author", feed.Author).CreateReader());
            extensions.Add(new XElement(itunesNS + "category",
                new XAttribute("text", feed.Category),
                new XElement(itunesNS + "category",
                    new XAttribute("text", "subcategoria"))).CreateReader());
            if (feed.Explicit)
                extensions.Add(new XElement(itunesNS + "explicit", "yes").CreateReader());
            else
                extensions.Add(new XElement(itunesNS + "explicit", "no").CreateReader());
            extensions.Add(new XDocument(
                new XElement(itunesNS + "owner",
                new XElement(itunesNS + "name", feed.Author),
                new XElement(itunesNS + "email", feed.Email))).CreateReader());

            var feedItems = new List<SyndicationItem>();
            foreach (var episode in feed.Episodes)
            {
                var item = new SyndicationItem(episode.Title, null, new Uri(episode.MediaUrl));
                item.Summary = new TextSyndicationContent(episode.Description);
                item.Id = episode.ShowNumber.ToString();

                if (episode.PublishedAt != null)
                    item.PublishDate = episode.PublishedAt;

                item.Links.Add(new SyndicationLink()
                {
                    Title = episode.Title,
                    Uri = new Uri(episode.MediaUrl),
                    Length = 99999999,       // TODO
                    MediaType = "media type" // TODO
                });

                var itemExt = item.ElementExtensions;
                itemExt.Add(new XElement(itunesNS + "subtitle", episode.Subtitle).CreateReader());
                itemExt.Add(new XElement(itunesNS + "summary", episode.Description).CreateReader());
                itemExt.Add(new XElement(itunesNS + "duration", string.Format("{0}:{1:00}:{2:00}",
                    0, 0, 0)).CreateReader()); // TODO
                itemExt.Add(new XElement(itunesNS + "keywords", "keywords").CreateReader()); // TODO
                if (episode.Explicit)
                    extensions.Add(new XElement(itunesNS + "explicit", "yes").CreateReader());
                else
                    extensions.Add(new XElement(itunesNS + "explicit", "no").CreateReader());
                itemExt.Add(new XElement("enclosure", new XAttribute("url", episode.MediaUrl),
                    new XAttribute("length", "length"), new XAttribute("type", "type"))); // TODO

                feedItems.Add(item);
            }

            synFeed.Items = feedItems;

            return synFeed;
        }
    }
}
