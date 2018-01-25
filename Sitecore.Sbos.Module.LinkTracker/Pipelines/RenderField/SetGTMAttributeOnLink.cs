using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.RenderField;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Sbos.Module.LinkTracker.Pipelines.RenderField
{
    public class SetGTMAttributeOnLink : SetTrackedAttributeOnLink
    {
        List<Item> gtm = new List<Item>();

        public override void Process(RenderFieldArgs args)
        {
            LoadGTM();

            if (!this.CanProcess(args))
            {
                return;
            }

            string shouldTriggerGTM;

            if (!string.IsNullOrEmpty(this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.CampaignTriggerAttName)))
            {
                shouldTriggerGTM = this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.CampaignTriggerAttName) == "1" ? "true" : "false";
            }
            else
            {
                shouldTriggerGTM = "false";
            }

            if(shouldTriggerGTM == "true")
            {
                args.Result.FirstPart = this.AddOrExtendAttributeValue(args.Result.FirstPart, "onclick", "triggerGTM('" + this.GetXmlAttributeValue(args.FieldValue, this.XmlAttributeName) + "', '" + shouldTriggerGTM + "', '" + this.GetXmlAttributeValue(args.FieldValue, LinkTrackerConstants.GTMAttributeName) + "',  '" + gtm[0].Fields["EventData"].ToString() + "');");
            }           
        }
        public void LoadGTM()
        {
            Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            gtm = master.SelectItems("fast:" + "/sitecore/system/Settings/Analytics/Page Events/Google Tag Manager//*[@@templateid='059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E']").Where(x => x.Fields["EventData"].Value.ToString() != string.Empty).ToList();
        }
    }
}