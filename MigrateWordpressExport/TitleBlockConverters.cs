using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateWordpressExport
{
    public static class TitleBlockConverters
    {
        public static string SimpleMarkdown(TitleBlock title)
        {
            var result = new StringBuilder();
            result.AppendFormat("# {0}\n\n## by {1}, published on {2}\n", title.Title, title.Author, title.PublicationDate);

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <remarks>
        /// ---
        /// layout: post
        /// title:  "Deploying Flynn Clusters on Azure"
        /// author: "Felix Rieseberg"
        /// author-link: "http://www.felixrieseberg.com"
        /// #author-image: "{{ site.baseurl }}/images/FelixRieseberg/photo.jpg" //should be square dimensions
        /// date:   2015-08-30 10:00:00
        /// categories: Azure DevOps Flynn
        /// color: "blue"
        /// #image: "{{ site.baseurl }}/images/imagename.png" #should be ~350px tall
        /// excerpt: "Deploying Flynn Clusters on Azure"
        /// ---
        /// </remarks>
        public static string JekyllHeaderMarkdown(TitleBlock title)
        {
            var result = new StringBuilder();
            result.AppendLine("---");
            result.AppendLine("layout: post");
            result.AppendFormat("title: \"{0}\"\n", title.Title);
            result.AppendFormat("author: \"{0}\"\n", title.Author);
            if (title.AuthorLink != null)
                result.AppendFormat("author-link: \"{0}\"\n", title.AuthorLink);
            if (title.AuthorImage != null)
                result.AppendFormat("#author-image: \"{{ site.baseurl }}/{0}\"\n", title.AuthorImage);
            result.AppendFormat("date: {0}\n", title.PublicationDate.ToString("yyyy-MM-dd HH:mm:SS"));
            if (title.Categories != null && title.Categories.Any())
                result.AppendFormat("categories: {0}\n", string.Join(" ", title.Categories));
            result.AppendLine("color: \"blue\"");
            if (title.ExcerptImage != null)
                result.AppendFormat("#image: \"{{ site.baseurl }}\"/{0}/\"\n", title.ExcerptImage);
            if (title.Excerpt != null)
                result.AppendFormat("excerpt: \"{0}\"\n", title.Excerpt);
            result.AppendLine("---");

            return result.ToString();
        }
    }
}
