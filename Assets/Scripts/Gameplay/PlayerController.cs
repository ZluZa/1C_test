using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Coroutine _gameplayCoroutine;

    public void StartPlaying()
    {
        _gameplayCoroutine = StartCoroutine(GameplayCoroutine());
    }

    public void StopPlaying()
    {
       StopCoroutine(_gameplayCoroutine);
    }
    
    IEnumerator GameplayCoroutine()
    {
        if (_gameplayCoroutine != null)
            StopCoroutine(_gameplayCoroutine);
        while (true)
        {
            if (Ke)
            yield return new WaitForEndOfFrame();
        }
    }
}
