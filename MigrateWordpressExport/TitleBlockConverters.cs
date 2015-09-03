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
    }
}
