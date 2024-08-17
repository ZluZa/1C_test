using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenElement : CanvasElement
{
    public override IEnumerator Init()
    {
        ShowElement(false, delegate
        {
            CoreGame.Instance.onManagersInitialized.AddListener(() => HideElement(true, delegate {  }));
        });
        yield return base.Init();
    }
}
