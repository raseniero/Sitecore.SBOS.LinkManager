using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Sitecore.Analytics;
using Sitecore.Analytics.Data.Items;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Analytics.Tracking;
using System;
using Sitecore.Analytics.Data;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Handler
{
    public class TrackedLinkHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable => false;
        public bool campaigncheck = false;

        [HttpGet]
        public void ProcessRequest(HttpContext context)
        {
            this.HandleQueryStringParameter(context, "triggerCampaign", "cid", "campaignData");
            this.HandleQueryStringParameter(context, "triggerGoal", "gid", "goalData");
            this.HandleQueryStringParameter(context, "triggerPageEvent", "peid", "pageEventData");
        }

        private void HandleQueryStringParameter(HttpContext context, string triggerParam, string idParam, string dataParam)
        {
            var parameter = context.Request.QueryString[triggerParam];
            if (parameter != null)
            {
                bool shouldTrigger;
                bool.TryParse(parameter, out shouldTrigger);

                
                if(triggerParam != "triggerCampaign")
                {
                    if (shouldTrigger)
                    {
                        var id = context.Request.QueryString[idParam];
                        var data = context.Request.QueryString[dataParam];
                        this.TriggerEvent(id, data);
                    }
                }
                if(triggerParam == "triggerCampaign")
                {
                    if (shouldTrigger)
                    {
                        var cid = context.Request.QueryString[idParam];
                        var cdata = context.Request.QueryString[dataParam];
                        this.TriggerCampaign(cid, cdata);
                    }

                }
            }

        }

        private void TriggerEvent(string id, string data)
        {
            ID scId;

            if (!string.IsNullOrEmpty(id) && ID.TryParse(id, out scId))
            {
                if (Tracker.IsActive == false)
                {                    
                    Tracker.StartTracking();               
                }
                if (Tracker.Current.CurrentPage != null && Tracker.Current.Interaction != null)
                {

                        //PageEvent
                        Item defItem = Context.Database.GetItem(scId);
                        var eventToTrigger = new PageEventData(defItem.Name, scId.Guid)
                        {
                            Data = data
                        };
                        Tracker.Current.CurrentPage.Register(eventToTrigger);
                }
            }
        }

         private void TriggerCampaign(string cid, string cdata)
         {
             ID scId;

             if (!string.IsNullOrEmpty(cid) && ID.TryParse(cid, out scId))
             {
                 if (Tracker.IsActive == false)
                 {
                    Tracker.StartTracking();
                 }

                 if (Tracker.Current.CurrentPage != null && Tracker.Current.Interaction != null)
                 {                     
                     Item campaignItem = Context.Database.GetItem(cid);
                     CampaignItem campaignToTrigger = new CampaignItem(campaignItem);
                     Tracker.Current.CurrentPage.TriggerCampaign(campaignToTrigger);
                }
             }
         }       
    }
}
