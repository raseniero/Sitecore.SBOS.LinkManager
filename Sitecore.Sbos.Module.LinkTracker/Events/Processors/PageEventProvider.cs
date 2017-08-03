using Sitecore.Data;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public class PageEventProvider : TrackedEventProviderProcessor
    {
        protected override string DefinitionItemPath => LinkTrackerConstants.SitecorePageEventPath;
        protected override ID TemplateId => LinkTrackerConstants.PageEventTemplateId;
        protected override int Index => 2;
    }
}