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
using System.Reflection;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;

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

        protected Checkbox TriggerGTM;

        protected Listbox GTM;

        protected Listbox GTMEvents;

        protected Button TestGTM;

        string gtmValueItem;

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

            this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMAttributeName] = XmlUtil.GetAttribute(LinkTrackerConstants.GTMAttributeName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMTriggerAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.GTMTriggerAttName, node);
            this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMEventAttName] = XmlUtil.GetAttribute(LinkTrackerConstants.GTMEventAttName, node);
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadControls();
            Update_GTMEvents();
            Update_GTM();
            Assert.ArgumentNotNull(e, "e");
            base.OnLoad(e);
            if (Context.ClientPage.IsEvent)
            {
                return;
            }
            
            if (!Context.ClientPage.IsPostBack)
            {
                //reload();
                //Update_Listbox();
            }
            //LoadControls();
            
            //TestGTM.OnClick += new EventHandler(Button_Click);
        }

        public List<Item> GetDefinitionItems(string path, string tempId)
        {
            var context = Configuration.Factory.GetDatabase("master");
            Item item = context.SelectSingleItem(path);
            List<Item> items = item.Axes.GetDescendants().Where(x => x.TemplateID.ToString() == tempId).ToList();
            return items;
        }

        protected virtual void reload()
        {
            string webRooPath = this.GetWebRootPath("Sitecore.Sbos.Module.LinkTracker");

            string[] PathElement = new string[4];
            PathElement[1] = "/sitecore/system/Marketing Control Panel/Goals";
            PathElement[2] = "/sitecore/system/Settings/Analytics/Page Events";
            PathElement[3] = "/sitecore/system/Marketing Control Panel/Campaigns";

            PathElement[4] = "/sitecore/system/Settings/Analytics/Page Events/Google Tag Manager";

            string[] TempId = new string[4];
            TempId[1] = "{475E9026-333F-432D-A4DC-52E03B75CB6B}";
            TempId[2] = "{059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E}";
            TempId[3] = "{94FD1606-139E-46EE-86FF-BC5BF3C79804}";

            TempId[4] = "{059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E}";

            for (int i = 1; i <= 3; i++)
            {
                var ItemsOn = GetDefinitionItems(PathElement[i].ToString(), TempId[i].ToString());

                if(ItemsOn != null)
                { 
                    if(i != 4)
                    { 
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                        XmlNodeList nodeList = xdoc.GetElementsByTagName("Combobox");
              
                        if (nodeList.Count > 1)
                        {
                            XmlElement ElementList = (XmlElement)nodeList[i];
                            ElementList.IsEmpty = true;

                            foreach (var item in ItemsOn)
                            {
                                var itemName = item.Name;
                                var itemId = item.ID;

                                XmlElement listItem = xdoc.CreateElement("ListItem");

                                listItem.SetAttribute("Value", itemId.ToString());
                                listItem.SetAttribute("Header", itemName);
                                listItem.RemoveAttribute("xmlns");

                                ElementList.AppendChild(listItem);
                            }
                            xdoc.Save(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                        }
                    }
                    else
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                        XmlNodeList nodeList = xdoc.GetElementsByTagName("Listbox");

                        XmlElement gtmElement = (XmlElement)nodeList[0];
                        gtmElement.IsEmpty = true;


                        foreach (var item in ItemsOn)
                        {
                            var itemName = item.DisplayName;
                            var itemId = item.ID;
                            XmlElement ListItem = xdoc.CreateElement("ListItem");

                            ListItem.SetAttribute("Value", itemId.ToString());
                            ListItem.SetAttribute("Header", itemName);
                            ListItem.RemoveAttribute("xmlns");

                            gtmElement.AppendChild(ListItem);

                        }
                        xdoc.Save(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    }
                }
            }
        }


        private string GetWebRootPath(string assembly)
        {
            string assemblyLoc = Assembly.Load(assembly).CodeBase;

            if (!string.IsNullOrEmpty(assemblyLoc))
            {
                assemblyLoc = assemblyLoc.Replace("file:///", "");

                string webRootPath = assemblyLoc.Replace(Data.Constants.LinkTrackerConstants.AssemblyLinkTrackerPath,
                    "");

                return webRootPath;
            }

            return string.Empty;
        }

        public int dblClickCount = 0;
        public string dblClickValue = "";

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

            string gtmValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMAttributeName];
            string gtmTriggerValue = this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMTriggerAttName];
            string gtmEvent = this.AnalyticsLinkAttributes[LinkTrackerConstants.GTMEventAttName];

            this.GTM.Value = gtmValue;
            this.TriggerGTM.Value = gtmTriggerValue;
            this.GTMEvents.Value = gtmEvent;

        }

        public void Update_GTMEvents()
        {
            string[] SplitGTMEvents = this.GTMEvents.Value.Split(',');

            foreach (var item in SplitGTMEvents)
            {
                var gtmEventItem = GTM.Items.Where(x => x.Value.Equals(item));
                if (gtmEventItem.Count() > 0)
                {
                    this.GTMEvents.Controls.Add(gtmEventItem.First());
                }
            }
            foreach (var controlItem in this.GTMEvents.Items)
            {
                this.GTM.Controls.Remove(controlItem);
            }
        }

        public void Update_GTM()
        {
            string[] SplitGTM = this.GTM.Value.Split(',');

            foreach (var item in SplitGTM)
            {
                var gtmItem = GTMEvents.Items.Where(x => x.Value.Equals(item));
                if (gtmItem.Count() > 0)
                {
                    this.GTM.Controls.Add(gtmItem.First());
                }
            }
            foreach (var controlItem in this.GTM.Items)
            {
                this.GTMEvents.Controls.Remove(controlItem);
            }
        }

        //public void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    for(int i = 0; i < GTM.Items.Count(); i++)
        //    {
        //        if (GTM.SelectedItem.Value.Equals(GTM.Items[i].Value))
        //        {
        //            this.GTMEvents.Controls.Add(GTM.Items[i]);
        //        }     
        //    }            
        //}
        //public void Button_Click(object sender, EventArgs e)
        //{

        //    for (int i = 0; i < GTM.Items.Count(); i++)
        //    {
        //        if (GTM.SelectedItem.Value.Equals(GTM.Items[i].Value))
        //        {
        //            this.GTMEvents.Controls.Add(GTM.Items[i]);
        //        }
        //    }
        //}

        //public void clickme()
        //{
        //    for (int i = 0; i < GTM.Items.Count(); i++)
        //    {
        //        if (GTM.SelectedItem.Value.Equals(GTM.Items[i].Value))
        //        {
                    
        //            this.GTMEvents.Controls.Add(GTM.Items[i]);
        //            if (string.IsNullOrEmpty(this.GTMEvents.Value))
        //            {
        //                this.GTMEvents.Value = gtmValueItem;
        //            }
        //            else
        //            {
        //                this.GTMEvents.Value += "," + gtmValueItem;

        //            }
        //        }
        //    }
        //    //foreach (var yControlItem in this.GTMEvents.Items)
        //    //{
        //    //    this.GTM.Controls.Remove(yControlItem);
        //    //}
        //}
        public void GTM_ClickItem()
        {
            if(this.GTM.Items.Count() < 1)
            {
                SheerResponse.Alert("GTM Selected Value: Empty");
            }
            else
            { 
                SheerResponse.Alert("GTM Selected Value: " + this.GTM.SelectedItem.Header.ToString());
            }
        }
        public void GTMEvents_ClickItem()
        {
            if (this.GTMEvents.Items.Count() < 1)
            {
                SheerResponse.Alert("GTMEvents Selected Value: Empty");
            }
            else
            {
                SheerResponse.Alert("GTMEvents Selected Value: " + this.GTMEvents.SelectedItem.Header.ToString());
            }
        }
        protected virtual void TryLang()
        {
           
            if (GTMEvents.Items.Count() == 0)
            {
                GTMEvents.Value = string.Empty;
            }
            string gtmValueItem = this.GTM.SelectedItem.Value;

            for (int ycounter = 0; ycounter < GTM.Items.Count(); ycounter++)
            {

                if (gtmValueItem == this.GTM.Items[ycounter].Value)
                {
                    SheerResponse.Alert("Add from events: " + this.GTM.Items[ycounter].Header);
                    this.GTMEvents.Controls.Add(GTM.Items[ycounter]);
                    if (string.IsNullOrEmpty(this.GTMEvents.Value))
                    {
                        this.GTMEvents.Value = gtmValueItem;
                    }
                    else
                    {
                        this.GTMEvents.Value += "," + gtmValueItem;
                    }
                }
            }

        }
        protected virtual void TryLang2()
        {
            if (GTM.Items.Count() == 0)
            {
                GTM.Value = string.Empty;
            }

            string gtmEventsValueItem = this.GTMEvents.SelectedItem.Value;

            for (int xcounter = 0; xcounter < GTMEvents.Items.Count(); xcounter++)
            {
                SheerResponse.Alert("Remove form events:" + this.GTMEvents.Items[xcounter].Header);
                if (gtmEventsValueItem == this.GTMEvents.Items[xcounter].Value)
                {
                    this.GTM.Controls.Add(GTMEvents.Items[xcounter]);
                    if (string.IsNullOrEmpty(this.GTM.Value))
                    {
                        this.GTM.Value = gtmEventsValueItem;
                    }
                    else
                    {
                        this.GTM.Value += "," + gtmEventsValueItem;
                    }

                }
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

            if(GTM.Items.Count() > 0)
            {   
                TryLang();
            }
            if(GTMEvents.Items.Count() > 0)
            {
                TryLang2();
            }

            //GTM
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GTMTriggerAttName, this.TriggerGTM);

            this.TrimListboxControl(this.GTM);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GTMAttributeName, this.GTM);

            this.TrimListboxControl(this.GTMEvents);
            LinkForm.SetAttribute(packet, LinkTrackerConstants.GTMEventAttName, this.GTMEvents);
            //GTM - END

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

        protected virtual void TrimListboxControl(Listbox control)
        {
            if (string.IsNullOrEmpty(control?.Value))
            {
                return;
            }

            control.Value = control.Value.Trim();
        }
        protected virtual void TrimGTMControl(Checkbox control)
        {
            if (string.IsNullOrEmpty(control?.Value))
            {
                return;
            }

            control.Value = control.Value.Trim();
        }
    }
}