using Sitecore.Data.Fields;

namespace Sitecore.Sbos.Module.LinkTracker.Data.Fields
{
    public class GoalLinkField : LinkField
    {
        public GoalLinkField(Field innerField)
            : base(innerField)
        {
        }

        public GoalLinkField(Field innerField, string runtimeValue)
            : base(innerField, runtimeValue)
        {
        }

        public string Goal
        {
            get
            {
                return GetAttribute("goal");
            }
            set
            {
                this.SetAttribute("goal", value);
            }
        }

        public string TriggerGoal
        {
            get
            {
                return GetAttribute("triggergoal");
            }
            set
            {
                this.SetAttribute("triggergoal", value);
            }
        }
    }
}