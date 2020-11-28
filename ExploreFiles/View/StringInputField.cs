using System;
using Bones.Scripts.Features.ExploreFiles.Presentation;
using TMPro;
using UnityEngine;

namespace Bones.Scripts.Features.ExploreFiles.View
{
    public class StringInputField : MonoBehaviour, SearchBar
    {
        [SerializeField] private TMP_InputField tmp;

        private void Start()
        {
            tmp.onValueChanged.AddListener(OnModify.Invoke);
        }

        public string Text
        {
            get => tmp.text;
            set => tmp.text = value;
        }

        public event Action<string> OnModify = _ => { };
    }
}