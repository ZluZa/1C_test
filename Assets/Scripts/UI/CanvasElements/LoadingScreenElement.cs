using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenElement : CanvasElement
{
    public override IEnumerator Init(CanvasManager cm)
    {
        yield return base.Init(cm);
        ShowElement(false, delegate
        {
            CoreGame.Instance.onManagersInitialized.AddListener(() => HideElement(true, delegate {  }));
        });

    }

    public override void ShowElement(bool animated, Action onComplete)
    {
        CanvasManager.isLoaderScreenActive = true;
        base.ShowElement(animated, delegate
        {
            CanvasManager.onLoaderScreenShown.Invoke();
            onComplete();
        });
    }

    public override void HideElement(bool animated, Action onComplete)
    {
        //ФЕЙКОВОЕ ОЖИДАНИЕ, ПРОСТО ДЛЯ ВИЗУАЛА
        
        StartCoroutine(FakeDelay());
        IEnumerator FakeDelay()
        {
            yield return new WaitForSeconds(1f);
            CanvasManager.isLoaderScreenActive = false;
            base.HideElement(animated, delegate
            {
                CanvasManager.onLoaderScreenHidden.Invoke();
                onComplete();
            });
        }
       
    }
}
