using System;
using System.Collections.Generic;
using Bones.Scripts.Features.ExploreFiles.Domain;
using Bones.Scripts.Features.ExploreFiles.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bones.Scripts.Features.ExploreFiles.View
{
    public class ExplorerView : MonoBehaviour, ExploreScreen
    {
        public event Action OnGoUp = () => { };

        [SerializeField] Button goUpButton;
        [Space] [SerializeField] TextMeshProUGUI currentDirectoryLabel;
        [SerializeField] DataButton directoryButtonPrefab;
        [SerializeField] DataButton fileButtonPrefab;
        [Space] [SerializeField] RectTransform directoriesContent;
        [SerializeField] RectTransform filesContent;
        [SerializeField] StringInputField searchBar;
        [Space] [SerializeField] string extension;
        [SerializeField] string preferredDirectory;


        private readonly Queue<Transform> childrenQueue = new Queue<Transform>();
        private ExplorerPresenter explorerPresenter;

        private float directoriesSpacing;
        private float filesSpacing;


        public string Extension => extension;
        public string PreferredDirectory => preferredDirectory;
        public SearchBar SearchBar => searchBar;


        private void Awake()
        {
            explorerPresenter = new ExplorerPresenter(this);

            goUpButton.onClick.AddListener(OnGoUp.Invoke);
        }

        private void Start()
        {
            directoriesSpacing = directoriesContent.GetComponent<VerticalLayoutGroup>().spacing;
            filesSpacing = filesContent.GetComponent<VerticalLayoutGroup>().spacing;

            explorerPresenter.Start();
        }

        private void OnDestroy()
        {
            explorerPresenter.Dispose();
        }

        public void PrepareDisplayDirectory(DirectoryInfo directoryToDisplay)
        {
            const float verticalPadding = 10;

            ClearChildren(directoriesContent.gameObject);
            SetSizeDelta(directoriesContent, verticalPadding, 1);


            ClearChildren(filesContent.gameObject);
            SetSizeDelta(filesContent, verticalPadding, 1);

            currentDirectoryLabel.text = directoryToDisplay.directoryPath;
        }

        public ExplorerItem AddDirectory(DirectoryInfo directoryInfo)
        {
            var directoryButton = Instantiate(directoryButtonPrefab, directoriesContent);
            AddSizeDelta(directoriesContent,
                (directoriesSpacing + directoryButton.RectTransform.sizeDelta.y) * Vector2.up);
            directoryButton.Text = directoryInfo.directoryName;
            return directoryButton;
        }

        public ExplorerItem AddFile(FileInfo fileInfo)
        {
            var fileButton = Instantiate(fileButtonPrefab, filesContent);
            AddSizeDelta(directoriesContent, (filesSpacing + fileButton.RectTransform.sizeDelta.y) * Vector2.up);
            fileButton.Text = fileInfo.fileName;
            return fileButton;
        }

        private void SetSizeDelta(RectTransform rectTransform, float value, int vectorComponent)
        {
            var sizeDelta = rectTransform.sizeDelta;
            sizeDelta[vectorComponent] = value;
            rectTransform.sizeDelta = sizeDelta;
        }

        private void AddSizeDelta(RectTransform rectTransform, Vector2 value)
        {
            rectTransform.sizeDelta += value;
        }

        private void ClearChildren(GameObject go)
        {
            var childCount = go.transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                childrenQueue.Enqueue(go.transform.GetChild(i));
            }

            while (childrenQueue.Count > 0)
            {
                Destroy(childrenQueue.Dequeue().gameObject);
            }
        }
    }
}