using Sitecore.Data;

namespace Sitecore.Sbos.Module.LinkTracker.Data.Constants
{
    public class LinkTrackerConstants
    {
        public const string ExternalFormPath = "/sitecore/shell/Applications/Dialogs/ExternalLink/ExternalLink.xml";

        public const string AssemblyLinkTrackerPath = "/bin/Sitecore.Sbos.Module.LinkTracker.DLL";

        public const string SitecoreGoalPath = "/sitecore/system/Marketing Control Panel/Goals";

        public const string SitecorePageEventPath = "/sitecore/system/Settings/Analytics/Page Events";

        public const string SitecoreCampaignPath = "/sitecore/system/Marketing Control Panel/Campaigns";

        public const string JQueryScript = "<script src='https://code.jquery.com/jquery-1.12.4.js' type='text/javascript' language='Javascript'></script>";

        public const string LinkTrackerMgrScript = "<script src='/scripts/js/LinkTrackerManager.js' type='text/javascript' language='Javascript'></script>";

        public const string LinkTrackerMgdJSPath = "/scripts/js/LinkTrackerManager.js";

        public const string GoalAttributeName = "goal";

        public const string GoalTriggerAttName = "triggergoal";

        public const string GoalDataAttName = "goaldata";

        public const string PageEventAttributeName = "pageevent";

        public const string PageEventTriggerAttName = "triggerpageevent";

        public const string PageEventDataAttName = "pageeventdata";

        public const string CampaignAttributeName = "campaign";

        public const string CampaignTriggerAttName = "triggercampaign";

        public const string CampaignDataAttName = "campaigndata";

        public static readonly ID GoalTemplateId = new ID("{475E9026-333F-432D-A4DC-52E03B75CB6B}");

        public static readonly ID PageEventTemplateId = new ID("{059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E}");

        public static readonly ID CampaignTemplateID = new ID("{94FD1606-139E-46EE-86FF-BC5BF3C79804}");
    }
}