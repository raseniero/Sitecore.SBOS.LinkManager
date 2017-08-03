using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetPageEventAttributeOnLink : SetTrackedAttributeOnLink
    {
        public override void Process(RenderFieldArgs args)
        {
            if (!this.CanProcess(args))
            {
                return;
            }

            string shouldTriggerPageEvent;

            if (!string.IsNullOrEmpty(this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.PageEventTriggerAttName)))
            {
                shouldTriggerPageEvent = this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.PageEventTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerPageEvent = "false";
            }

            args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "triggerPageEvent('" + this.GetXmlAttributeValue(args.FieldValue, this.XmlAttributeName) + "', '" + shouldTriggerPageEvent + "', '" + this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.PageEventDataAttName) + "');");
        }
    }
}