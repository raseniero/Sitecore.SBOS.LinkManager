using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetPageEventAttributeOnLink : SetEventAttributeOnLink
    {
        public override void Process(RenderFieldArgs args)
        {
            if (!CanProcess(args))
            {
                return;
            }

            string shouldTriggerPageEvent;

            if (!string.IsNullOrEmpty(GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.PageEventTriggerAttName)))
            {
                shouldTriggerPageEvent = GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.PageEventTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerPageEvent = "false";
            }

            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, AttributeName, GetXmlAttributeValue(args.FieldValue, XmlAttributeName));
            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, LinkTrackerConstants.PageEventTriggerAttName, shouldTriggerPageEvent);
            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "triggerPageEvent('" + GetXmlAttributeValue(args.FieldValue, XmlAttributeName) + "', '" + shouldTriggerPageEvent + "');");
        }
    }
}