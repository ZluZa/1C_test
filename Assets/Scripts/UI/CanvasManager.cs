using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : BaseManager
{
   private Dictionary<Type, CanvasElement> _canvasCls = new();
   
   public override IEnumerator Init()
   {
      foreach (var c in GetComponentsInChildren<CanvasElement>())
         _canvasCls.Add(c.GetType(), c);
      foreach (var c in _canvasCls.Values)
         yield return c.Init();
      yield return base.Init();
   }
}
