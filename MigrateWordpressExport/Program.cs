﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MigrateWordpressExport
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2) throw new ArgumentException("Must provide input file, output directory");

            var inputFile = args[0];
            var outputDir = args[1];
            var converter = new GithubMarkdownConverter() { TitleBlockConverter = TitleBlockConverters.SimpleMarkdown };
            foreach (var post in new WordpressExportParser().Parse(inputFile))
            {
                var contents = converter.Convert(post);
                var outputFile = Path.Combine(outputDir, post.TitleBlock.PublicationDate.ToString("yyyy-MM-dd-") + post.FriendlyName + ".md");
                Console.WriteLine("Writing '{0}' to {1}", post.TitleBlock.Title, outputFile);
                File.WriteAllText(outputFile, contents);
            }

            Console.WriteLine("\n============== Done ================");
            Console.ReadLine();
        }
    }
}
