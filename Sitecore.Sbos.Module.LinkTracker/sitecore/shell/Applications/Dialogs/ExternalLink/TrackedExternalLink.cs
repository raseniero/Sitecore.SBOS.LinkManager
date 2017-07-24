using System;
using System.Collections.Specialized;
using System.Xml;
using Sitecore.Diagnostics;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;
using Sitecore.Shell.Applications.Dialogs;
using Sitecore.Shell.Applications.Dialogs.ExternalLink;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Xml;

namespace Sitecore.Sbos.Module.LinkTracker.sitecore.shell.Applications.Dialogs.ExternalLink
{
    public class TrackedExternalLink : ExternalLinkForm
    {
        protected Combobox Goal;

        protected Checkbox TriggerGoal;

        protected Combobox PageEvent;

        protected Checkbox TriggerPageEvent;

        private NameValueCollection analyticsLinkAttributes;

        protected NameValueCollection AnalyticsLinkAttributes
        {
            get
            {
                if (analyticsLinkAttributes == null)
                {
                    analyticsLinkAttributes = new NameValueCollection();
                    ParseLinkAttributes(GetLink());
                }

                return analyticsLinkAttributes;
            }
        }

        protected override void ParseLink(string link)
        {
            base.ParseLink(link);
            ParseLinkAttributes(link);
        }

        protected virtual void ParseLinkAttributes(string link)
        {
            Assert.ArgumentNotNull(link, "link");
            XmlDocument xmlDocument = XmlUtil.LoadXml(link);

            XmlNode node = xmlDocument?.SelectSingleNode("/link");
            if (node == null)
            {
                return;
            }

            AnalyticsLinkAttributes[LinkTrackerConstants.GoalAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.GoalAttributeName, node);
            AnalyticsLinkAttributes[LinkTrackerConstants.GoalTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.GoalTriggerAttName, node);
            AnalyticsLinkAttributes[LinkTrackerConstants.PageEventAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.PageEventAttributeName, node);
            AnalyticsLinkAttributes[LinkTrackerConstants.PageEventTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.PageEventTriggerAttName, node);
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (Context.ClientPage.IsEvent)
            {
                return;
            }

            LoadControls();
        }

        protected virtual void LoadControls()
        {
            string goalValue = AnalyticsLinkAttributes[LinkTrackerConstants.GoalAttributeName];
            string triggerGoalValue = AnalyticsLinkAttributes[LinkTrackerConstants.GoalTriggerAttName];

            if (!string.IsNullOrWhiteSpace(goalValue))
            {
                Goal.Value = goalValue;
                TriggerGoal.Value = triggerGoalValue;
            }

            string pageEventValue = AnalyticsLinkAttributes[LinkTrackerConstants.PageEventAttributeName];
            string triggerPageEventValue = AnalyticsLinkAttributes[LinkTrackerConstants.PageEventTriggerAttName];

            if (!string.IsNullOrWhiteSpace(pageEventValue))
            {
                PageEvent.Value = pageEventValue;
                TriggerPageEvent.Value = triggerPageEventValue;
            }
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            string path = GetPath();
            string attributeFromValue = LinkForm.GetLinkTargetAttributeFromValue(Target.Value, CustomTarget.Value);
            Packet packet = new Packet("link");
            LinkForm.SetAttribute(packet, "text", Text);
            LinkForm.SetAttribute(packet, "linktype", "external");
            LinkForm.SetAttribute(packet, "url", path);
            LinkForm.SetAttribute(packet, "anchor", string.Empty);
            LinkForm.SetAttribute(packet, "title", Title);
            LinkForm.SetAttribute(packet, "class", Class);
            LinkForm.SetAttribute(packet, "target", attributeFromValue);

            TrimComboboxControl(Goal);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GoalTriggerAttName, TriggerGoal);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GoalAttributeName, Goal);

            TrimComboboxControl(PageEvent);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.PageEventTriggerAttName, TriggerPageEvent);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.PageEventAttributeName, PageEvent);

            SheerResponse.SetDialogValue(packet.OuterXml);
            SheerResponse.CloseWindow();
        }

        private string GetPath()
        {
            string url = this.Url.Value;
            if (url.Length > 0 && url.IndexOf("://", StringComparison.InvariantCulture) < 0 && !url.StartsWith("/", StringComparison.InvariantCulture))
            {
                url = string.Concat("http://", url);
            }

            return url;
        }

        protected virtual void TrimComboboxControl(Combobox control)
        {
            if (string.IsNullOrEmpty(control?.Value))
            {
                return;
            }

            control.Value = control.Value.Trim();
        }
    }
}