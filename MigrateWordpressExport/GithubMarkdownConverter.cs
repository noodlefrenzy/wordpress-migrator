using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MigrateWordpressExport
{
    public class GithubMarkdownConverter
    {
        public Func<TitleBlock, string> TitleBlockConverter { get; set; }

        private string ConvertElement(XElement element)
        {
            if (!element.HasElements) return (string)element;

            var result = new StringBuilder();
            foreach (var node in element.Nodes())
            {
                if (node is XText)
                {
                    result.Append(((XText)node).Value);
                } else if (node is XElement)
                {
                    var elt = (XElement)node;
                    switch (elt.Name.ToString())
                    {
                        case "a":
                            result.AppendFormat("[{0}]({1})", ConvertElement(elt), elt.Attribute("href").Value);
                            break;

                        case "img":
                            result.AppendFormat("![{0}]({1})", elt.Attribute("alt").Value, elt.Attribute("src").Value);
                            break;

                        case "code":
                            result.AppendFormat("`{0}`", (string)elt);
                            break;

                        default:
                            Trace.TraceWarning("Unrecognized sub-element {0}: {1}", elt.Name, elt);
                            result.Append((string)elt);
                            break;
                    }
                }
            }

            return result.ToString();
        }

        public string Convert(BlogPost post)
        {
            if (post == null || post.TitleBlock == null || string.IsNullOrWhiteSpace(post.Contents) || TitleBlockConverter == null)
            {
                throw new ArgumentException("Missing title/converter/post");
            }

            var doc = XDocument.Parse("<body>" + post.Contents.Replace("&nbsp;","") + "</body>");
            var result = new StringBuilder();
            result.AppendLine(this.TitleBlockConverter(post.TitleBlock));

            foreach (var node in doc.Root.Elements())
            {
                switch (node.Name.ToString())
                {
                    case "h1":
                        result.AppendFormat("# {0}\n\n", (string)node);
                        break;

                    case "h2":
                        result.AppendFormat("## {0}\n\n", (string)node);
                        break;

                    case "h3":
                        result.AppendFormat("### {0}\n\n", (string)node);
                        break;

                    case "h4":
                        result.AppendFormat("### {0}\n\n", (string)node);
                        break;

                    case "p":
                        result.AppendFormat("{0}\n\n", ConvertElement(node));
                        break;

                    case "pre":
                        result.AppendFormat("```\n{0}\n```\n\n", (string)node);
                        break;

                    default:
                        result.AppendFormat("Unknown ({0}): `{1}`", node.Name.ToString(), (string)node);
                        break;
                }
            }

            return result.ToString();
        }
    }
}
