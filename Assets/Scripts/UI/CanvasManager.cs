using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : BaseManager
{
   private Dictionary<Type, CanvasElement> _canvasCls = new();
   
   [HideInInspector] public bool isLoaderScreenActive;
   
   public UnityEvent onLoaderScreenShown;
   public UnityEvent onLoaderScreenHidden;
   
   public override IEnumerator Init()
   {
      onLoaderScreenShown.AddListener(delegate { Debug.Log("ShowLoading"); });
      onLoaderScreenHidden.AddListener(delegate { Debug.Log("HiddenLoading"); });
      foreach (var c in GetComponentsInChildren<CanvasElement>())
         _canvasCls.Add(c.GetType(), c);
      foreach (var c in _canvasCls.Values)
         yield return c.Init(this);
      yield return base.Init();
   }
}
