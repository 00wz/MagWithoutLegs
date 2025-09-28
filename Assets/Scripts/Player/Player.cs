using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private float _health;
    private IHUD _hud;
    private float _fireballCooldown;
    private float _healCooldown;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject hudObject;
    [SerializeField] private new Camera camera;
    [SerializeField] private SpriteRenderer bodySpriteRenderer;
    [SerializeField] private Transform[] handsRoots;
    [SerializeField] private SpriteRenderer[] handsSpriteRenderers;
    [SerializeField] private Spell fireball;
    [SerializeField] private Spell heal;
    
    public void TakeHealth(float value)
    {
        _health = Mathf.Clamp(_health + value, 0, maxHealth);
        _hud.SetHealth(_health);
        CheckForDeath();
    }

    public float GetHealth() => _health;
    
    public event Action OnDied;

    private void Awake()
    {
        _health = maxHealth;
        _hud = hudObject.GetComponent<IHUD>();
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
        _fireballCooldown = _fireballCooldown - Time.deltaTime >= 0 ? _fireballCooldown - Time.deltaTime : 0;
        _healCooldown = _healCooldown - Time.deltaTime >= 0 ? _healCooldown - Time.deltaTime : 0;
        
        if (Input.GetMouseButtonDown(0) && _fireballCooldown == 0)
            CastSpell(fireball);
        else if (Input.GetMouseButtonDown(1) && _healCooldown == 0)
            CastSpell(heal);
    }

    private void CastSpell(Spell spell)
    {
        var isFireball = spell.SpellName == "Fireball";

        if (isFireball)
        {
            _fireballCooldown = fireball.Cooldown;
            _hud.SetFireballCooldown();
        }
        else
        {
            _healCooldown = heal.Cooldown;
            _hud.SetHealCooldown();
        }

        var hand = handsRoots[isFireball ? 0 : 1];
        spell.Cast(hand.position, hand.forward);
    }

    private void CheckForDeath()
    {
        if (_health != 0) return;
        
        Debug.Log("Died");
        OnDied?.Invoke();
    }
}
