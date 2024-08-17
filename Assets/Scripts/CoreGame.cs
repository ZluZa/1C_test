using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CoreGame : MonoBehaviour
{
    public static CoreGame Instance { get; set; }
    
    private readonly Dictionary<Type, BaseManager> _managers = new();
    public Dictionary<Type, BaseManager> Managers => _managers;
     
    public UnityEvent onManagersInitialized;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        StartCoroutine(Init());
    }
    
    private IEnumerator Init()
    {
        foreach (var m in GetComponentsInChildren<BaseManager>())
            _managers.Add(m.GetType(), m);
        foreach (var m in _managers.Values)
        {
            yield return m.Init();
        }
        onManagersInitialized.Invoke();
    }
    public BaseManager GetManager(Type type)
    {
        return _managers[type];
    }
}