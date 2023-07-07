using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Convertors
{
    public static class TextFixer
    {
        public static string FixText(this string text) => text?.Trim().Replace("  ", " ");

        public static string FixEmail(string email) => email.Trim().ToLower().Replace(" ", "");

        public static string RemoveHtmlTagsExceptBreak(string text) => Regex.Replace(text, @"<(?!br[\x20/>])[^<>]+>", string.Empty);

        public static string ReplaceNewLineTextArea(string text) => text?.Replace(Environment.NewLine, "<br />");

        public static string ReplaceBrToNewLine(string text) => text?.Replace("<br />", Environment.NewLine);

        public static string InsertNofollowInHtml(string htmlText)
        {
            if (htmlText == null) return htmlText;

            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlText);
                var nodes = doc.DocumentNode.SelectNodes("//a");

                if (!nodes.Any()) return htmlText;

                foreach (var node in nodes)
                {
                    string outer = node.OuterHtml;
                    node.Attributes.Add("rel", "nofollow");
                    htmlText = htmlText.Replace(outer, node.OuterHtml);
                }
            }
            catch (Exception e)
            {

            }

            return htmlText;
        }

        public static string FixTextForUrl(this string text)
        {
            return text.Trim().Replace(' ', '-');
        }

        public static string ConvertBrToNewLine(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace("<br/>", Environment.NewLine);
        }

        public static string ConvertNewLineToBr(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Replace(Environment.NewLine, "<br/>");
        }

        public static string FixedEmail(this string email)
        {
            return email.Trim().ToLower();
        }

        public static string[] SplitTags(this string tags)
        {
            return tags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string FixTitleForUrl(this string url)
        {
            return url.Replace(" ", "-").Replace("+", "").Replace("#", "");
        }

        public static string FixUrlToTitle(this string title)
        {
            return title.Replace("-", " ");
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string GetFirstSectionOfString(this string text, int length = 360)
        {
            if (text.Length >= length)
            {
                return text.Substring(0, length) + "...";
            }

            return text;
        }

        public static string SetNoFollowLinkAttr(this string text)
        {
            var html = new HtmlDocument();
            html.LoadHtml(text);
            var result = html.Text;
            var anchors = html.DocumentNode.Descendants("A");

            foreach (var a in anchors.ToList())
            {
                var newNode = a.CloneNode(true);
                newNode.Attributes.Add("rel", "nofollow");
                var data = a.ParentNode.ReplaceChild(newNode, a);
                result = result.Replace(a.OuterHtml, data.OuterHtml);
            }

            return result;
        }

        public static string RemoveNoFollowLinkAttr(this string text)
        {
            var html = new HtmlDocument();
            html.LoadHtml(text);
            var result = html.Text;
            var anchors = html.DocumentNode.Descendants("A");

            foreach (var a in anchors.ToList())
            {
                var newNode = a.CloneNode(true);
                newNode.Attributes.Add("rel", "nofollow");
                var data = a.ParentNode.ReplaceChild(newNode, a);
                result = result.Replace(a.OuterHtml, data.OuterHtml);
            }

            return result;
        }
    }
}
