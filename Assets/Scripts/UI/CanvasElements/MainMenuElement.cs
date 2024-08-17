using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuElement : CanvasElement
{
    public override IEnumerator Init(CanvasManager cm)
    {
        yield return base.Init(cm);
        
        CanvasManager.onLoaderScreenHidden?.AddListener(() => ShowElement(true,
            delegate { CanvasManager.onLoaderScreenHidden.RemoveListener(() => ShowElement(true, delegate { })); }));
    }
}