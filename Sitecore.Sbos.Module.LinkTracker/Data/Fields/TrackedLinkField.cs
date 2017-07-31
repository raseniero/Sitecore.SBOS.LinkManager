﻿using Sitecore.Data.Fields;
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
            get => this.GetAttribute(LinkTrackerConstants.GoalAttributeName);
            set => this.SetAttribute(LinkTrackerConstants.GoalAttributeName, value);
        }

        public string TriggerGoal
        {
            get => this.GetAttribute(LinkTrackerConstants.GoalTriggerAttName);
            set => this.SetAttribute(LinkTrackerConstants.GoalTriggerAttName, value);
        }

        public string GoalData
        {
            get => this.GetAttribute(LinkTrackerConstants.GoalDataAttName);
            set => this.SetAttribute(LinkTrackerConstants.GoalDataAttName, value);
        }

        public string PageEvent
        {
            get => this.GetAttribute(LinkTrackerConstants.PageEventAttributeName);
            set => this.SetAttribute(LinkTrackerConstants.PageEventAttributeName, value);
        }

        public string TriggerPageEvent
        {
            get => this.GetAttribute(LinkTrackerConstants.PageEventTriggerAttName);
            set => this.SetAttribute(LinkTrackerConstants.PageEventTriggerAttName, value);
        }

        public string PageEventData
        {
            get => this.GetAttribute(LinkTrackerConstants.PageEventDataAttName);
            set => this.SetAttribute(LinkTrackerConstants.PageEventDataAttName, value);
        }
    }
}