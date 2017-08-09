using Sitecore.Data;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public class CampaignProvider : TrackedEventProviderProcessor
    {
        protected override string DefinitionItemPath => LinkTrackerConstants.SitecoreCampaignPath;
        protected override ID TemplateId => LinkTrackerConstants.CampaignTemplateID;
        protected override int Index => 3;
    }
}