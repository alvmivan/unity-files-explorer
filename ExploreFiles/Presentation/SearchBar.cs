using System;

namespace Bones.Scripts.Features.ExploreFiles.Presentation
{
    public interface SearchBar
    {
        string Text { get; set; }
        event Action<string> OnModify;
    }
}