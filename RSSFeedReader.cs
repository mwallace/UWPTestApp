﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace RSSFeedReader
{
    class RSSFeedReader
    {
        private string _rssResult = "Null";

        public string RSSResult
        {
            get { return _rssResult; }
            set { _rssResult = value; }
        }
        public async Task downloadRSSFeedAsync(string url, ObservableCollection<RSSLink> RSSLinks)
        {
            Windows.Web.Syndication.SyndicationClient client = new SyndicationClient();

            Uri feedUri
                = new Uri(url);
            
            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // The rest of this method executes after await RetrieveFeedAsync completes.
                RSSResult = feed.Title.Text + Environment.NewLine;

                foreach (SyndicationItem item in feed.Items)
                {
                    RSSResult = item.Title.Text + ", " +
                                     item.PublishedDate.ToString() + Environment.NewLine;
                    string link = string.Empty;
                    if (item.Links.Count > 0)
                    {
                        link = item.Links[0].Uri.AbsoluteUri;
                    }
                    
                    RSSLinks.Add(new RSSLink() { Name = RSSResult, Link = link });
                }
                
            }
            catch (Exception ex)
            {
                // Log Error.
                RSSResult =
                    "I'm sorry, but I couldn't load the page," +
                    " possibly due to network problems." +
                    "Here's the error message I received: "
                    + ex.ToString();
            }
        }
    }
}
