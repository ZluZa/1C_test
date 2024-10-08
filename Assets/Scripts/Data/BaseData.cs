using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData : ScriptableObject
{
    [SerializeField] private Sprite objectSprite;
    [SerializeField] private string objectName;

    public Sprite ObjectSprite => objectSprite;
    public string ObjectName => objectName;

}
