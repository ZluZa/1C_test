using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasElement : MonoBehaviour
{
    protected CanvasManager CanvasManager;
    
    private const string FallbackShowAnimation = "DefaultCanvasElementShow";
    private const string FallbackHideAnimation = "DefaultCanvasElementHide";
    
    [SerializeField] protected string _showAnimation;
    [SerializeField] protected string _hideAnimation;

    private Animation _animation;

    private Transform _parent;
    private CanvasGroup _canvasGroup;

    private Coroutine _showHideCoroutine;

    public virtual IEnumerator Init(CanvasManager cm)
    {
        CanvasManager = cm;
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _animation = GetComponent<Animation>();
        _parent = _canvasGroup.transform;
        HideElement(false, delegate {  });
        yield return null;
    }

    public virtual void ShowElement(bool animated, Action onComplete)
    {
        if (animated)
        {
            string animationName = _showAnimation != String.Empty ? _showAnimation : FallbackShowAnimation;
            if (_showHideCoroutine != null)
                StopCoroutine(_showHideCoroutine);
            _showHideCoroutine = StartCoroutine(Animate(animationName, onComplete));
        }
        else
        {
            _parent.gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            onComplete();
        }
    }

    public virtual void HideElement(bool animated, Action onComplete)
    {
        if (animated)
        {
            string animationName = _showAnimation != String.Empty ? _hideAnimation : FallbackHideAnimation;
            if (_showHideCoroutine != null)
                StopCoroutine(_showHideCoroutine);
            _showHideCoroutine = StartCoroutine(Animate(animationName, onComplete));
        }
        else
        {
            _parent.gameObject.SetActive(false);
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            onComplete();
        }
    }
    
    private IEnumerator Animate(string animationName, Action onComplete)
    {
        _animation.Play(animationName);
        yield return new WaitForSeconds(_animation.GetClip(animationName).length);
        onComplete();
    }
}
