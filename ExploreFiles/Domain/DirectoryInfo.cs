namespace Bones.Scripts.Features.ExploreFiles.Domain
{
    public readonly struct DirectoryInfo
    {
        public readonly string directoryPath;
        public readonly string directoryName;

        internal DirectoryInfo(string directoryPath, string directoryName)
        {
            this.directoryPath = directoryPath;
            this.directoryName = directoryName;
        }
    }
}