using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateWordpressExport
{
    /// <summary>
    /// Meant to encapsulate the information common to title blocks for posts.
    /// </summary>
    /// <remarks>
    /// And in particular, the title blocks for the Catalyst team's blog:
    /// 
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
    public class TitleBlock
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorLink { get; set; }
        public string AuthorImage { get; set; }
        public DateTime PublicationDate { get; set; }
        public string[] Categories { get; set; }
        public string ExcerptImage { get; set; }
        public string Excerpt { get; set; }
    }
}
