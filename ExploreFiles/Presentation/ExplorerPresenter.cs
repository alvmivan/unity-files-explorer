using Bones.Scripts.Features.ExploreFiles.Domain;
using Bones.Scripts.Features.ExploreFiles.Logic;
using UnityEngine;

namespace Bones.Scripts.Features.ExploreFiles.Presentation
{
    public class ExplorerPresenter
    {
        private const string LastDirectoryStoredKey = "Bones.FileExplorer.LastDirectoryStored";

        private readonly ExploreScreen screen;
        private readonly SearchBar searchBar;

        private DirectoryInfo current;

        private readonly FilesManager explorer = new FilesManager();

        public ExplorerPresenter(ExploreScreen screen)
        {
            this.screen = screen;
            searchBar = screen.SearchBar;
        }


        public void Start()
        {
            Bind();
            current = PickAnInitialDirectory();
            Present();
        }


        public void Dispose()
        {
            Unbind();
        }

        private void Bind()
        {
            screen.OnGoUp += OnGoUp;
            searchBar.OnModify += OnSearchBarChanges;
        }


        private void Unbind()
        {
            screen.OnGoUp -= OnGoUp;
            searchBar.OnModify -= OnSearchBarChanges;
        }

        private void OnSearchBarChanges(string _)
        {
            Present();
        }

        private void Present()
        {
            DisplayDirectory(current);
        }

        private void DisplayDirectory(DirectoryInfo info)
        {
            current = info;
            StoreDirectory(info);
            screen.PrepareDisplayDirectory(info);
            var directories = explorer.GetChildrenDirectories(info, searchBar.Text);
            foreach (var directoryInfo in directories)
                screen
                    .AddDirectory(directoryInfo)
                    .OnCLick(() => DisplayDirectory(directoryInfo));

            var files = explorer.GetFiles(info, screen.Extension, searchBar.Text);
            foreach (var fileInfo in files) screen.AddFile(fileInfo); //todo: bind with show file content
        }

        private void OnGoUp()
        {
            DisplayDirectory(explorer.GetParentDirectory(current));
        }

        private DirectoryInfo PickAnInitialDirectory()
        {
            var stored = PlayerPrefs.GetString(LastDirectoryStoredKey);
            var favDir = screen.PreferredDirectory;
            DirectoryInfo dir;
            if (!string.IsNullOrEmpty(stored) && explorer.ValidateDirectory(stored, out dir)) return dir;
            if (!string.IsNullOrEmpty(favDir) && explorer.ValidateDirectory(favDir, out dir)) return dir;
            return explorer.Root();
        }

        private void StoreDirectory(DirectoryInfo directory)
        {
            PlayerPrefs.SetString(LastDirectoryStoredKey, directory.directoryPath);
            PlayerPrefs.Save();
        }
    }
}