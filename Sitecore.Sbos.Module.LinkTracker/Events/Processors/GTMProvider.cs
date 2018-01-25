using Sitecore.Data;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public class GTMProvider : TrackedEventProviderProcessor
    {
        protected override string DefinitionItemPath => LinkTrackerConstants.SitecoreGTM;
        protected override ID TemplateId => LinkTrackerConstants.PageEventTemplateId;
        protected override int Index => 4;
    }
}