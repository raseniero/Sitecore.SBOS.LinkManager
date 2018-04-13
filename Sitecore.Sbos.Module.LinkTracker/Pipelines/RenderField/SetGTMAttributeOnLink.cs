using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Sbos.Module.LinkTracker.sitecore.shell.Applications.Dialogs.ExternalLink;
using System.Net;
using System.Collections.Specialized;
using System.Data;
using Sitecore.Analytics.Pipelines.GetRenderingRules;
using Sitecore.Shell.Applications.ContentEditor;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetGTMAttributeOnLink : SetTrackedAttributeOnLink
    {
        List<Item> gtm = new List<Item>();
        List<string> counter = new List<string>();

        public override void Process(RenderFieldArgs args)
        {
            LoadGTM();

            if (!this.CanProcess(args))
            {
                return;
            }

            string shouldTriggerGTM;


            if (!string.IsNullOrEmpty(this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GTMTriggerAttName)))
            {
                shouldTriggerGTM = this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GTMTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerGTM = "false";
            }

            if(shouldTriggerGTM == "true")
            {
                var gtmEventsValue = this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GTMEventAttName);
                string[] gtmEventsValueList = gtmEventsValue.Split(',');
                for (int count = 0; count < gtmEventsValueList.Count(); count++)
                {
                    for (int x = 0; x < gtm.Count(); x++)
                    {
                        if (gtm[x].ID.ToString() == gtmEventsValueList[count].Trim())
                        {
                            string[] EventData = gtm[x].Fields["EventData"].ToString().Split('&');
                            string[] DataValue = new string[EventData.Count()];
                            string InsertData, ConcatData = "";
                            string DataLayer = "dataLayer.push({, 'event' : '" + gtm[x].Fields["EventName"].ToString() + "'});";

                            for (int nxt = 0; nxt < EventData.Count(); nxt++)
                            {
                                string[] Splitter = WebUtility.UrlDecode(EventData[nxt]).Split('=');

                                if (Splitter.Count() == 1)
                                {
                                    DataValue[nxt] = "'" + Splitter[0] + "' : '" + "'";
                                }
                                if (Splitter.Count() == 2)
                                {
                                    DataValue[nxt] = "'" + Splitter[0] + "' : '" + Splitter[1] + "'";
                                }

                            }
                            for (int value = 0; value < DataValue.Count(); value++)
                            {
                                if (DataValue[value] == DataValue[0])
                                {
                                    ConcatData += DataValue[value];
                                }
                                else
                                {
                                    ConcatData += ", " + DataValue[value];
                                }
                            }
                            InsertData = DataLayer.Insert(16, ConcatData);

                            //args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", InsertData);
                       
                                args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", InsertData);
                                //    string[] EventData = gtm[x].Fields["EventData"].ToString().Split('&');
                                //    string[] EventCategory = WebUtility.UrlDecode(EventData[0].ToString()).Split('=');
                                //    string[] EventAction = WebUtility.UrlDecode(EventData[1].ToString()).Split('=');
                                //    string[] EventLabel = WebUtility.UrlDecode(EventData[2].ToString()).Split('=');
                                //    string[] EventValue = WebUtility.UrlDecode(EventData[3].ToString()).Split('=');
                                //    string[] EventAdditional = WebUtility.UrlDecode(EventData[4].ToString()).Split('=');

                                //    //args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "dataLayer.push({'" + gtmEventsValueList[count].Trim() + "', '" + shouldTriggerGTM + "', '" + EventCategory[0] + "' : '" + EventCategory[1] + "', '" + EventAction[0] + "' : '" + EventAction[1] + "', '" + EventLabel[0] + "': '" + EventLabel[1] + "', '" + EventValue[0] + "' : ' ', '" + EventAdditional[0] + "' : ' '});");
                                //    args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "dataLayer.push({'" + EventCategory[0] + "' : '" + EventCategory[1] + "', '" + EventAction[0] + "' : '" + EventAction[1] + "', '" + EventLabel[0] + "': '" + EventLabel[1] + "', '" + EventValue[0] + "' : ' ', '" + EventAdditional[0] + "' : ' '});");
                            }
                    }
                }
            }           
        }
        public void LoadGTM()
        {
            Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            gtm = master.SelectItems("fast:" + "/sitecore/system/Settings/Analytics/Page Events/Google Tag Manager//*[@@templateid='059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E']").Where(x => x.Fields["EventData"].Value.ToString() != string.Empty).ToList();
        }
    }
}