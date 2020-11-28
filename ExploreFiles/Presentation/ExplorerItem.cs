using System;

namespace Bones.Scripts.Features.ExploreFiles.Presentation
{
    public interface ExplorerItem
    {
        void OnCLick(Action onClick);
    }
}