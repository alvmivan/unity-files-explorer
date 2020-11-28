using System;
using Bones.Scripts.Features.ExploreFiles.Domain;

namespace Bones.Scripts.Features.ExploreFiles.Presentation
{
    public interface ExploreScreen
    {
        string Extension { get; }
        string PreferredDirectory { get; }
        SearchBar SearchBar { get; }
        void PrepareDisplayDirectory(DirectoryInfo info);
        ExplorerItem AddDirectory(DirectoryInfo info);
        ExplorerItem AddFile(FileInfo info);
        event Action OnGoUp;
    }
}