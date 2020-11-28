namespace Bones.Scripts.Features.ExploreFiles.Domain
{
    
    public readonly struct FileInfo
    {
        public readonly string filePath;
        public readonly string fileName;

        internal FileInfo(string filePath, string fileName)
        {
            this.filePath = filePath;
            this.fileName = fileName;
        }
    }
}