using System.Xml;
using Sitecore.Pipelines.RenderField;
using Sitecore.Xml;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetGoalAttributeOnLink
    {
        private string GoalXmlAttributeName { get; set; }

        private string GoalAttributeName { get; set; }

        private string BeginningHtml { get; set; }

        public void Process(RenderFieldArgs args)
        {
            if (!CanProcess(args))
            {
                return;
            }

            string isTriggerGoal = string.Empty;

            if (!string.IsNullOrEmpty(GetXmlAttributeValue(args.FieldValue, "triggergoal")))
            {
                if (GetXmlAttributeValue(args.FieldValue, "triggergoal") == "1")
                {
                    isTriggerGoal = "true";
                }
                else
                {
                    isTriggerGoal = "false";
                }
            }
            else
            {
                isTriggerGoal = "false";
            }

            args.Result.FirstPart = AddGoalAttributeValue(args.Result.FirstPart, GoalAttributeName, GetXmlAttributeValue(args.FieldValue, GoalXmlAttributeName));
            args.Result.FirstPart = AddGoalAttributeValue(args.Result.FirstPart, "triggergoal", isTriggerGoal);
            args.Result.FirstPart = AddGoalAttributeValue(args.Result.FirstPart, "onclick", "activateSelectedGoal('" + GetXmlAttributeValue(args.FieldValue, GoalXmlAttributeName) + "', '" + isTriggerGoal + "');");
        }

        protected virtual bool CanProcess(RenderFieldArgs args)
        {
            return !string.IsNullOrWhiteSpace(GoalAttributeName)
                    && !string.IsNullOrWhiteSpace(BeginningHtml)
                    && !string.IsNullOrWhiteSpace(GoalXmlAttributeName)
                    && args != null
                    && args.Result != null
                    && HasXmlAttributeValue(args.FieldValue, GoalAttributeName)
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
            if (xmlDocument == null)
            {
                return string.Empty;
            }

            XmlNode node = xmlDocument.SelectSingleNode("/link");
            if (node == null)
            {
                return string.Empty;
            }

            return XmlUtil.GetAttribute(attributeName, node);
        }

        protected virtual string AddGoalAttributeValue(string html, string attributeName, string attributeValue)
        {
            if (string.IsNullOrWhiteSpace(html) || string.IsNullOrWhiteSpace(attributeName) || string.IsNullOrWhiteSpace(attributeValue))
            {
                return html;
            }

            int index = html.LastIndexOf(">");
            if (index < 0)
            {
                return html;
            }

            string firstPart = html.Substring(0, index);
            string attribute = string.Format(" {0}=\"{1}\"", attributeName, attributeValue);
            string lastPart = html.Substring(index);
            return string.Concat(firstPart, attribute, lastPart);
        }
    }
}