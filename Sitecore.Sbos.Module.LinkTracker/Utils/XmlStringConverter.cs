using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Sitecore.Analytics.Pipelines.StartTracking;

namespace Sitecore.Sbos.Module.LinkTracker.Utils
{
    public class XmlStringConverter
    {
        public static string RemoveInvalidXmlChars(string text)
        {
            var validXmlChars = text.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray();
            return new string(validXmlChars);
        }

        public static string EscapeInvalidXmlChars(string text)
        {
            var encodedXmlString = XmlConvert.EncodeName(text);

            return encodedXmlString;
        }
    }
}