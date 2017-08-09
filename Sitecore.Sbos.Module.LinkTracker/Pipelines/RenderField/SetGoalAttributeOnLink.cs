using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetGoalAttributeOnLink : SetTrackedAttributeOnLink
    {
        public override void Process(RenderFieldArgs args)
        {
            if (!this.CanProcess(args))
            {
                return;
            }

            string shouldTriggerGoal;

            if (!string.IsNullOrEmpty(this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GoalTriggerAttName)))
            {
                shouldTriggerGoal = this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GoalTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerGoal = "false";
            }
            if(shouldTriggerGoal == "true")
            {
                args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "triggerGoal('" + this.GetXmlAttributeValue(args.FieldValue, this.XmlAttributeName) + "', '" + shouldTriggerGoal + "', '" + this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GoalDataAttName) + "');");
            }           
        }
    }
}