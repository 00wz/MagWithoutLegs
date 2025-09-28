using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IHUD hud;
    private List<BaseEnemy> _enemies;
    private PlayerController _player;
    private BasePrincess _princess;
    private bool _endGame = false;
    void Start()
    {
        _enemies = new List<BaseEnemy>(FindObjectsByType<BaseEnemy>(FindObjectsSortMode.None));

        foreach (var e in _enemies)
        {
            Action deadEvent = null;
            deadEvent = () =>
            {
                e.OnDied -= deadEvent;
                _enemies.Remove(e);
                if (_enemies.Count <= 0)
                {
                    OnWin();
                }
            };
            e.OnDied += deadEvent;
        }

        _player = FindAnyObjectByType<PlayerController>();
        if (_player != null)
        {
            _player.OnDied += OnLose;
        }
        _princess = FindAnyObjectByType<BasePrincess>();
        if (_princess != null)
        {
            _princess.OnDied+= OnLose;
        }
    }

    private void OnLose()
    {
        if (_endGame)
        {
            return;
        }
        _endGame = true;
    }

    private void OnWin()
    {
        if (_endGame)
        {
            return;
        }
        _endGame = true;

    }
}
