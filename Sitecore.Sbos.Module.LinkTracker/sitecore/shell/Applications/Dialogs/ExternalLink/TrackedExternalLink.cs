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

        protected Edit GoalData;

        protected Combobox PageEvent;

        protected Checkbox TriggerPageEvent;

        protected Edit PageEventData;

        protected Combobox Campaign;

        protected Checkbox TriggerCampaign;

        protected Edit CampaignData;
        
        private NameValueCollection analyticsLinkAttributes;

        protected NameValueCollection AnalyticsLinkAttributes
        {
            get
            {
                if (this.analyticsLinkAttributes == null)
                {
                    this.analyticsLinkAttributes = new NameValueCollection();
                    this.ParseLinkAttributes(this.GetLink());
                }

                return this.analyticsLinkAttributes;
            }
        }

        protected override void ParseLink(string link)
        {
            base.ParseLink(link);
            this.ParseLinkAttributes(link);
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

            this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.GoalAttributeName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.GoalTriggerAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalDataAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.GoalDataAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.PageEventAttributeName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.PageEventTriggerAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventDataAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.PageEventDataAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.CampaignAttributeName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.CampaignTriggerAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignDataAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.CampaignDataAttName, node);
        }

        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (Context.ClientPage.IsEvent)
            {
                return;
            }

            this.LoadControls();
        }

        protected virtual void LoadControls()
        {
            string goalValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalAttributeName];
            string triggerGoalValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalTriggerAttName];
            string goalDataValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.GoalDataAttName];

            if (!string.IsNullOrWhiteSpace(goalValue))
            {
                this.Goal.Value = goalValue;
                this.TriggerGoal.Value = triggerGoalValue;
                this.GoalData.Value = goalDataValue;
            }

            string pageEventValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventAttributeName];
            string triggerPageEventValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventTriggerAttName];
            string pageEventDataValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.PageEventDataAttName];

            if (!string.IsNullOrWhiteSpace(pageEventValue))
            {
                this.PageEvent.Value = pageEventValue;
                this.TriggerPageEvent.Value = triggerPageEventValue;
                this.PageEventData.Value = pageEventDataValue;
            }

            string campaignValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignAttributeName];
            string campaignTriggerValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignTriggerAttName];
            string campaignDataValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.CampaignDataAttName];

            if (!string.IsNullOrWhiteSpace(campaignValue))
            {
                this.Campaign.Value = campaignValue;
                this.TriggerCampaign.Value = campaignTriggerValue;
                this.CampaignData.Value = campaignDataValue;
            }
        }

        protected override void OnOK(object sender, EventArgs args)
        {
            Assert.ArgumentNotNull(sender, "sender");
            Assert.ArgumentNotNull(args, "args");
            string path = this.GetPath();
            string attributeFromValue = LinkForm.GetLinkTargetAttributeFromValue(this.Target.Value, this.CustomTarget.Value);
            Packet packet = new Packet("link");
            LinkForm.SetAttribute(packet, "text", this.Text);
            LinkForm.SetAttribute(packet, "linktype", "external");
            LinkForm.SetAttribute(packet, "url", path);
            LinkForm.SetAttribute(packet, "anchor", string.Empty);
            LinkForm.SetAttribute(packet, "title", this.Title);
            LinkForm.SetAttribute(packet, "class", this.Class);
            LinkForm.SetAttribute(packet, "target", attributeFromValue);

            this.TrimComboboxControl(this.Goal);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GoalTriggerAttName, this.TriggerGoal);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GoalAttributeName, this.Goal);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GoalDataAttName, this.GoalData);
    
            this.TrimComboboxControl(this.PageEvent);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.PageEventTriggerAttName, this.TriggerPageEvent);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.PageEventAttributeName, this.PageEvent);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.PageEventDataAttName, this.PageEventData);

            this.TrimComboboxControl(this.Campaign);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.CampaignTriggerAttName, this.TriggerCampaign);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.CampaignAttributeName, this.Campaign);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.CampaignDataAttName, this.CampaignData);

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