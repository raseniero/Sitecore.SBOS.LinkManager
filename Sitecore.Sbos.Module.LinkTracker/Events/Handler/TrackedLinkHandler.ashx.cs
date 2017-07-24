using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Sitecore.Analytics;
using Sitecore.Analytics.Data.Items;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Analytics.Tracking;
using System;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Handler
{
    public class TrackedLinkTrackerHandler : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable => false;

        [HttpGet]
        public void ProcessRequest(HttpContext context)
        {
            this.HandleQueryStringParameter(context, "triggerGoal", "gid");
            this.HandleQueryStringParameter(context, "triggerPageEvent", "peid");
        }

        private void HandleQueryStringParameter(HttpContext context, string triggerParam, string idParam)
        {
            var parameter = context.Request.QueryString[triggerParam];

            bool shouldTrigger;
            bool.TryParse(parameter, out shouldTrigger);

            if (shouldTrigger)
            {
                var id = context.Request.QueryString[idParam];
                TriggerEvent(id);
            }
        }
        
        private void TriggerEvent(string id)
        {
            ID scId;

            if (!string.IsNullOrEmpty(id) && ID.TryParse(id, out scId))
            {
                if (Tracker.IsActive == false)
                {
                    Tracker.StartTracking();
                }

                if (Tracker.Current != null && Tracker.Current.Interaction != null && Tracker.Current.Interaction.PageCount > 0)
                {
                    Item defItem = Context.Database.GetItem(scId);
                    var eventToTrigger = new PageEventItem(defItem);

                    IPageContext desiredPage = null;

                    for (int i = Tracker.Current.Interaction.PageCount; i > 0 && desiredPage == null; i--)
                    {
                        var page = Tracker.Current.Interaction.GetPage(i);
                        if (page.Item != null && Guid.Empty.Equals(page.Item.Id) == false)
                        {
                            desiredPage = page;
                        }
                    }

                    desiredPage?.Register(eventToTrigger);
                    Tracker.Current.CurrentPage.Cancel();
                }
            }
        }
    }
}