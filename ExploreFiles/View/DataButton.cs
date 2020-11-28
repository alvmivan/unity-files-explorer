using System;
using Bones.Scripts.Features.ExploreFiles.Presentation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bones.Scripts.Features.ExploreFiles.View
{
    public class DataButton : MonoBehaviour, ExplorerItem
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;

        public string Text
        {
            get => label.text;
            set => label.text = value;
        }

        public RectTransform RectTransform => transform as RectTransform;

        public void OnCLick(Action onClick)
        {
            button.onClick.AddListener(onClick.Invoke);
        }
    }
}