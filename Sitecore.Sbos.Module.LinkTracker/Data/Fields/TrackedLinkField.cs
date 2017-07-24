using Sitecore.Data.Fields;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Data.Fields
{
    public class TrackedLinkField : LinkField
    {
        public TrackedLinkField(Field innerField)
            : base(innerField)
        {
        }

        public TrackedLinkField(Field innerField, string runtimeValue)
            : base(innerField, runtimeValue)
        {
        }

        public string Goal
        {
            get
            {
                return GetAttribute(LinkTrackerConstants.GoalAttributeName);
            }
            set
            {
                this.SetAttribute(LinkTrackerConstants.GoalAttributeName, value);
            }
        }

        public string TriggerGoal
        {
            get
            {
                return GetAttribute(LinkTrackerConstants.GoalTriggerAttName);
            }
            set
            {
                this.SetAttribute(LinkTrackerConstants.GoalTriggerAttName, value);
            }
        }

        public string PageEvent
        {
            get
            {
                return GetAttribute(LinkTrackerConstants.PageEventAttributeName);
            }
            set
            {
                this.SetAttribute(LinkTrackerConstants.PageEventAttributeName, value);
            }
        }

        public string TriggerEvent
        {
            get
            {
                return GetAttribute(LinkTrackerConstants.PageEventTriggerAttName);
            }
            set
            {
                this.SetAttribute(LinkTrackerConstants.PageEventTriggerAttName, value);
            }
        }
    }
}