using System.Collections.Generic;

namespace Bones.Scripts.Features.ExploreFiles.Domain
{
    public interface ExploreFiles
    {
        bool ValidateDirectory(string path, out DirectoryInfo directoryInfo);
        DirectoryInfo Root();
        DirectoryInfo GetParentDirectory(DirectoryInfo directory);
        List<FileInfo> GetFiles(DirectoryInfo directory, string extension, string search);
        List<DirectoryInfo> GetChildrenDirectories(DirectoryInfo directory, string search);
    }
}