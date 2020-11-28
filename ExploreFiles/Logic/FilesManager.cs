using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bones.Scripts.Features.ExploreFiles.Domain;
using UnityEngine;
using DirectoryInfo = Bones.Scripts.Features.ExploreFiles.Domain.DirectoryInfo;
using FileInfo = Bones.Scripts.Features.ExploreFiles.Domain.FileInfo;

namespace Bones.Scripts.Features.ExploreFiles.Logic
{
    public class FilesManager : Domain.ExploreFiles, LoadSaveFiles
    {
        public bool ValidateDirectory(string path, out DirectoryInfo directoryInfo)
        {
            directoryInfo = NewDirectoryInfo("");
            if (Directory.Exists(path))
            {
                directoryInfo = NewDirectoryInfo(path);
                return true;
            }

            return false;
        }

        public DirectoryInfo Root()
        {
            //return NewDirectoryInfo("C:/");
            return NewDirectoryInfo(Application.persistentDataPath);
        }

        public DirectoryInfo GetParentDirectory(DirectoryInfo directory)
        {
            return NewDirectoryInfo(Directory.GetParent(directory.directoryPath).FullName);
        }

        public List<FileInfo> GetFiles(DirectoryInfo directory, string extension, string search)
        {
            var path = directory.directoryPath;
            return Directory
                .EnumerateFiles(path)
                .Where(s => s.EndsWith(extension))
                .Select(NewFileInfo)
                .Where(fi => fi.fileName.ToLower().Contains(search.ToLower()))
                .ToList();
        }

        public List<DirectoryInfo> GetChildrenDirectories(DirectoryInfo directory, string search)
        {
            var path = directory.directoryPath;
            return Directory
                .EnumerateDirectories(path)
                .Select(NewDirectoryInfo)
                .Where(di => di.directoryPath.ToLower().Contains(search.ToLower()))
                .ToList();
        }

        public bool Save(FileInfo fileInfo, string content, bool append)
        {
            if (!File.Exists(fileInfo.filePath)) return false;
            File.WriteAllText(fileInfo.filePath, content);
            return true;
        }

        public string Load(FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.filePath)) return "";
            return File.ReadAllText(fileInfo.filePath);
        }

        public FileInfo Create(DirectoryInfo directoryInfo, string fileName)
        {
            var fullPath = Path.Combine(directoryInfo.directoryPath, fileName);
            var fileInfo = NewFileInfo(fullPath);
            if (!File.Exists(fileInfo.filePath)) File.Create(fileInfo.filePath).Close();
            return fileInfo;
        }

        private DirectoryInfo NewDirectoryInfo(string path)
        {
            return new DirectoryInfo(path, GetDirectoryName(path));
        }

        private FileInfo NewFileInfo(string path)
        {
            return new FileInfo(path, GetFileName(path));
        }

        private string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        private string GetDirectoryName(string path)
        {
            return Path.GetFileName(path);
        }
    }
}