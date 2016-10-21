using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace UrgentCast.Engines
{
    public class SyndicationFeedResult : ContentResult
    {
        public SyndicationFeedResult(SyndicationFeed feed) : base()
        {
            using (var memStream = new MemoryStream())
            using (var writer = new XmlTextWriter(memStream, System.Text.UTF8Encoding.UTF8))
            {
                feed.SaveAsRss20(writer);
                writer.Flush();
                memStream.Position = 0;
                Content = new StreamReader(memStream).ReadToEnd();
                ContentType = "application/rss+xml";
            }
        }
    }
}
