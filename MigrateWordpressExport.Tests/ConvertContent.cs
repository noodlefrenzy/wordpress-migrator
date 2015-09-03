using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MigrateWordpressExport.Tests
{
    [TestClass]
    public class ConvertContent
    {
        [TestMethod]
        public void ConvertPostWithNoCode()
        {
            var title = new TitleBlock()
            {
                Author = "Bob Dobbs",
                PublicationDate = new DateTime(2010, 2, 3, 12, 13, 14, DateTimeKind.Utc),
                Title = "Post, Apocalyptic"
            };

            var postWithNoCode = Properties.Resources.PostWithNoCode;
            var converter = new GithubMarkdownConverter()
            {
                TitleBlockConverter = TitleBlockConverters.SimpleMarkdown
            };

            var result = converter.Convert(new BlogPost() { TitleBlock = title, Contents = postWithNoCode });
            Assert.AreEqual(Properties.Resources.PostWithNoCode_AsGitHubMarkdown, result);
        }
    }
}
