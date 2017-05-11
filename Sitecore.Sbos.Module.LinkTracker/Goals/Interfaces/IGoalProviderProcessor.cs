using Sitecore.Pipelines;

namespace Sitecore.Sbos.Module.LinkTracker.Goals.Interfaces
{
    public interface IGoalProviderProcessor
    {
        void Process(PipelineArgs args);
    }
}
