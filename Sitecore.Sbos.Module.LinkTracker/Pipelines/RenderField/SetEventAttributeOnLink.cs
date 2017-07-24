using System;
using System.Xml;
using Sitecore.Pipelines.RenderField;
using Sitecore.Xml;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public abstract class SetEventAttributeOnLink
    {
        protected string XmlAttributeName { get; set; }

        protected string AttributeName { get; set; }

        protected string BeginningHtml { get; set; }

        public abstract void Process(RenderFieldArgs args);

        protected virtual bool CanProcess(RenderFieldArgs args)
        {
            return !string.IsNullOrWhiteSpace(AttributeName)
                   && !string.IsNullOrWhiteSpace(BeginningHtml)
                   && !string.IsNullOrWhiteSpace(XmlAttributeName)
                   && args != null
                   && args.Result != null
                   && HasXmlAttributeValue(args.FieldValue, AttributeName)
                   && !string.IsNullOrWhiteSpace(args.Result.FirstPart)
                   && args.Result.FirstPart.ToLower().StartsWith(BeginningHtml.ToLower());
        }

        protected virtual bool HasXmlAttributeValue(string linkXml, string attributeName)
        {
            return !string.IsNullOrWhiteSpace(GetXmlAttributeValue(linkXml, attributeName));
        }

        protected virtual string GetXmlAttributeValue(string linkXml, string attributeName)
        {
            XmlDocument xmlDocument = XmlUtil.LoadXml(linkXml);

            XmlNode node = xmlDocument?.SelectSingleNode("/link");
            if (node == null)
            {
                return string.Empty;
            }

            return XmlUtil.GetAttribute(attributeName, node);
        }

        protected virtual string AddOrExtendAttributeValue(string html, string attributeName, string attributeValue)
        {
            if (string.IsNullOrWhiteSpace(html) || string.IsNullOrWhiteSpace(attributeName) || string.IsNullOrWhiteSpace(attributeValue))
            {
                return html;
            }

            int index = html.LastIndexOf(">", StringComparison.Ordinal);
            if (index < 0)
            {
                return html;
            }

            string existingAttrivute = $"{attributeName}=\"";
            string firstPart, attribute, lastPart;
            int existingAttributeIndex = html.IndexOf(existingAttrivute, StringComparison.OrdinalIgnoreCase);
            if (existingAttributeIndex >= 0)
            {
                int endofExistingAttributeIndex = html.IndexOf("\"", existingAttributeIndex + existingAttrivute.Length,
                    StringComparison.OrdinalIgnoreCase);
                firstPart = html.Substring(0, endofExistingAttributeIndex);
                attribute = attributeValue;
                lastPart = html.Substring(endofExistingAttributeIndex);
                return string.Concat(firstPart, attribute, lastPart);
            }

            firstPart = html.Substring(0, index);
            attribute = $" {attributeName}=\"{attributeValue}\"";
            lastPart = html.Substring(index);
            return string.Concat(firstPart, attribute, lastPart);
        }
    }
}