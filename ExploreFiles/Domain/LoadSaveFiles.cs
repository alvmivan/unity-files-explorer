namespace Bones.Scripts.Features.ExploreFiles.Domain
{
    public interface LoadSaveFiles
    {
        bool Save(FileInfo fileInfo, string content, bool append);
        string Load(FileInfo fileInfo);
        FileInfo Create(DirectoryInfo directoryInfo, string fileName);
    }
}