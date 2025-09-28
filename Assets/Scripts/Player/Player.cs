using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private float _health;
    private IHUD _hud;
    private int _currentSpellIndex;
    private float[] _spellsCooldowns;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject hudObject;
    [SerializeField] private new Camera camera;
    [SerializeField] private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private Transform[] handsRoots;
    [SerializeField] private SpriteRenderer[] handsSpriteRenderers;
    [SerializeField] private Spell[] spells;
    [SerializeField] private KeyCode spellChangingKey = KeyCode.Q;
    
    public void TakeHealth(float value)
    {
        _health = Mathf.Clamp(_health + value, 0, maxHealth);
        Debug.Log($"Received {value} health. All: {_health}");
        //_hud.SetHealth(_health);
        CheckForDeath();
    }

    public float GetHealth() => _health;
    
    public event Action OnDied;

    private void Awake()
    {
        _health = maxHealth;
        // Открыть при разработке интерфейса.
        //_hud = hudObject.GetComponent<IHUD>();
        
        _spellsCooldowns = new float[spells.Length];
    }
    
    private void Update()
    {
        RotateHands();
        ProcessInput();
    }

    private void RotateHands()
    {
        var worldCursorPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        worldCursorPosition.z = 0;
        
        var cursorOnRight = worldCursorPosition.x - transform.position.x >= 0;
        bodySpriteRenderer.flipX = !cursorOnRight;
        for (int i = 0; i < handsRoots.Length; i++)
        {
            handsRoots[i].LookAt(worldCursorPosition);
            handsSpriteRenderers[i].flipX = !cursorOnRight;
        }
    }

    private void ProcessInput()
    {
        for (int i = 0; i < _spellsCooldowns.Length; i++)
            _spellsCooldowns[i] = _spellsCooldowns[i] - Time.deltaTime >= 0 ? _spellsCooldowns[i] - Time.deltaTime : 0;

        if (Input.GetKeyDown(spellChangingKey))
        {
            _currentSpellIndex = _currentSpellIndex + 1 == spells.Length ? 0 : _currentSpellIndex + 1;
            Debug.Log($"Switched to {spells[_currentSpellIndex].SpellName}");
        }
        
        if (Input.GetMouseButtonDown(0) && _spellsCooldowns[_currentSpellIndex] == 0)
            CastSpell();
    }

    private void CastSpell()
    {
        var spell = spells[_currentSpellIndex];
        _spellsCooldowns[_currentSpellIndex] = spell.Cooldown;

        var hand = handsRoots[_currentSpellIndex % 2];
        spell.Cast(hand.position, hand.forward);
    }

    private void CheckForDeath()
    {
        if (_health != 0) return;
        
        Debug.Log("Died");
        OnDied?.Invoke();
    }
}
