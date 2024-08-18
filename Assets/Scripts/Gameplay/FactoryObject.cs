
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class FactoryObject : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        public virtual FactoryObject Init(BaseData data)
        {
            if (_image != null && data.ObjectSprite != null)
                _image.sprite = data.ObjectSprite;
            if (_text != null && data.ObjectName != String.Empty)
                _text.text = data.ObjectName;
            return this;
        }
    }