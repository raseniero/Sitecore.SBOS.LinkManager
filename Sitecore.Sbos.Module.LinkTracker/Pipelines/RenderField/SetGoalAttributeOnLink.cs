using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetGoalAttributeOnLink : SetEventAttributeOnLink
    {
        public override void Process(RenderFieldArgs args)
        {
            if (!CanProcess(args))
            {
                return;
            }

            string shouldTriggerGoal;

            if (!string.IsNullOrEmpty(GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GoalTriggerAttName)))
            {
                shouldTriggerGoal = GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GoalTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerGoal = "false";
            }

            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, AttributeName, GetXmlAttributeValue(args.FieldValue, XmlAttributeName));
            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, LinkTrackerConstants.GoalTriggerAttName, shouldTriggerGoal);
            args.Result.FirstPart = AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "triggerGoal('" + GetXmlAttributeValue(args.FieldValue, XmlAttributeName) + "', '" + shouldTriggerGoal + "');");
        }
    }
}