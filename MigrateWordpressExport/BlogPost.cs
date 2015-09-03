using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateWordpressExport
{
    public class BlogPost
    {
        public TitleBlock TitleBlock { get; set; }

        public string Contents { get; set; }

        public string FriendlyName { get; set; }
    }
}
