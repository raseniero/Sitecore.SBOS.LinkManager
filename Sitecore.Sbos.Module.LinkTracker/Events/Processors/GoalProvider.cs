using Sitecore.Data;
using Sitecore.Sbos.Module.LinkTracker.Data.Constants;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public class GoalProvider : TrackedEventProviderProcessor
    {
        protected override string DefinitionItemPath => LinkTrackerConstants.SitecoreGoalPath;
        protected override ID TemplateId => LinkTrackerConstants.GoalTemplateId;
        protected override int Index => 1;
    }
}