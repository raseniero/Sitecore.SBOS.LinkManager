using System.Web;
using Sitecore.Analytics;
using Sitecore.Analytics.Data.Items;
using Sitecore.Analytics.Model;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Sitecore.Sbos.Module.LinkTracker.Goals.Handler
{
    /// <summary>
    /// Summary description for GoalLinkTrackerHandler1
    /// </summary>
    public class GoalLinkTrackerHandler1 : IHttpHandler, IRequiresSessionState
    {
        [HttpGet]
        public void ProcessRequest(HttpContext context)
        {                                                                                                                                                                                                   
            var gid = context.Request.QueryString["gid"];
            var triggerGoal = context.Request.QueryString["triggerGoal"];

            bool isTriggerGoal = false;
            bool.TryParse(triggerGoal.ToString(), out isTriggerGoal);

            if (!string.IsNullOrEmpty(gid.ToString()) && isTriggerGoal)
            {
                if (!Tracker.IsActive)
                {
                    Tracker.StartTracking();
                }

                if (Tracker.IsActive)
                {
                    if (Tracker.Current.CurrentPage != null)
                    {
                        var id = new Sitecore.Data.ID(gid.ToString());
                        Item goalItem = Context.Database.GetItem(id);
                        var goalToTrigger = new PageEventItem(goalItem);

                        if (goalToTrigger != null)
                        {
                            PageEventData eventData = Tracker.Current.CurrentPage.Register(goalToTrigger);

                            Console.WriteLine("Goal is successfully triggered.");
                        }
                        else
                        {
                            Log.Error("Goal with ID " + gid + " does not exist", this);
                        }
                    }
                    else
                    {
                        Log.Error("Tracker.Current.CurrentPage is null", this);
                    }
                }
                else
                {
                    Log.Warn("The tracker is not active. Unable to register the goal.", this);
                }
            }
            else
            {
                Log.Warn("There is no goal selected.", this);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}