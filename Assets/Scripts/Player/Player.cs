using System;
using UnityEngine;

public sealed class Player : MonoBehaviour, IDamageable
{
    private float _health;
    private IHUD _hud;
    private float _handsXOffset;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject hudObject;
    [SerializeField] private Transform handsRoot;
    [SerializeField] private new Camera camera;
    [SerializeField] private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private SpriteRenderer handsSpriteRenderer;
    
    public void TakeHealth(float value)
    {
        _health = Mathf.Clamp(_health + value, 0, maxHealth);
        _hud.SetHealth(_health);
    }

    public float GetHealth() => _health;
    
    public event Action OnDied;

    private void Awake()
    {
        _health = maxHealth;
        _handsXOffset = handsRoot.localPosition.x;
        // Открыть при разработке интерфейса.
        //_hud = hudObject.GetComponent<IHUD>();
    }
    
    private void Update()
    {
        RotateHands();
    }

    private void RotateHands()
    {
        var worldCursorPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        worldCursorPosition.z = 0;
        handsRoot.LookAt(worldCursorPosition);
        
        var cursorOnRight = worldCursorPosition.x - transform.position.x >= 0;
        bodySpriteRenderer.flipX = !cursorOnRight;
        handsSpriteRenderer.flipX = !cursorOnRight;
        handsRoot.localPosition = new Vector3(_handsXOffset * (cursorOnRight ? 1 : -1), handsRoot.localPosition.y, 0);
    }

    private void CheckForDeath()
    {
        if (_health == 0)
            OnDied?.Invoke();
    }
}
