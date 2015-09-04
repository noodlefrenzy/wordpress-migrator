using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MigrateWordpressExport
{
    public class WordpressExportParser
    {
        public static readonly XNamespace ContentNamespace = "http://purl.org/rss/1.0/modules/content/";
        public static readonly XNamespace DCNamespace = "http://purl.org/dc/elements/1.1/";
        public static readonly XNamespace WPNamespace = "http://wordpress.org/export/1.2/";

        public TitleBlock TitleBlockOverrides { get; set; }

        public IEnumerable<BlogPost> Parse(string exportFile)
        {
            var doc = XDocument.Load(exportFile);
            return from x in doc.Descendants("item") select Convert(x);
        }

        private BlogPost Convert(XElement postXml)
        {
            try
            {
                var title = this.TitleBlockOverrides ?? new TitleBlock();
                title.Title = (string)postXml.Descendants("title").First();
                title.PublicationDate = DateTime.Parse((string)postXml.Descendants("pubDate").First());
                title.Categories = postXml.Descendants("category").Select(x => (string)x).ToArray();
                // Now for the override-able fields
                if (title.Author == null)
                    title.Author = (string)postXml.Descendants(DCNamespace + "creator").First();

                var post = new BlogPost();
                post.TitleBlock = title;
                post.Contents = (string)postXml.Descendants(ContentNamespace + "encoded").First();
                post.FriendlyName = (string)postXml.Descendants(WPNamespace + "post_name").First();

                return post;
            } catch (Exception e)
            {
                Trace.TraceError("Error converting {0}\n\n{1}", postXml, e);
                throw;
            }
        }
    }
}
