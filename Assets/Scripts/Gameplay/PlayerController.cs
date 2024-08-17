using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Coroutine _gameplayCoroutine;
    private Vector2 leftRightBorders;
    [SerializeField] private PlayerData _data;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerMovement;
    [SerializeField] private Transform _playerImage;
    [SerializeField] private Transform _viewTarget;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        var rect = GetComponent<RectTransform>().rect;
        leftRightBorders = new Vector2(rect.xMin, rect.xMax);
        StartPlaying();
    }
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
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = _playerMovement.position;
                float positionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                pos = new Vector3(positionX, pos.y);
                _playerMovement.position = pos;
            }
            else
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    yield return new WaitForEndOfFrame();
                if (Input.GetKey(KeyCode.A))
                {
                    _playerMovement.localPosition = new Vector2(_playerMovement.localPosition.x - _data.MovementSpeed,
                        _playerMovement.localPosition.y);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    _playerMovement.localPosition = new Vector2(_playerMovement.localPosition.x + _data.MovementSpeed,
                        _playerMovement.localPosition.y);
                }
            }

            float clampedPos = Mathf.Clamp(_playerMovement.localPosition.x, leftRightBorders.x, leftRightBorders.y);
            _playerMovement.localPosition = new Vector3(clampedPos, _playerMovement.localPosition.y); 
            AnimatePlayer();
            yield return new WaitForEndOfFrame();
        }
    }

    private void AnimatePlayer()
    {
        _player.up = _viewTarget.localPosition - _player.localPosition;
        float playerX = _player.localPosition.x;
        float targetX = _viewTarget.localPosition.x;
        playerX = Mathf.Lerp(playerX, _playerMovement.localPosition.x, Time.deltaTime * _data.MovementSpeed);
        targetX = Mathf.Lerp(targetX, playerX, Time.deltaTime * _data.MovementSpeed);
        _player.localPosition = new Vector2(playerX, _player.localPosition.y);
        _playerMovement.localPosition = _player.localPosition;
        _viewTarget.localPosition = new Vector2(targetX, _viewTarget.localPosition.y);

    }
}