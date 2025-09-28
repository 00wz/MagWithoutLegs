using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour, IHUD
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float maxPlayerHealth;
    [SerializeField] private float maxTargetHealth;
    [SerializeField] private Spell fireballSpell;
    [SerializeField] private Spell healSpell;

    private UnityEngine.UIElements.ProgressBar _healthBar;
    private UnityEngine.UIElements.ProgressBar _targetHealthBar;
    private Label _fireballCooldownLabel;
    private Label _healCooldownLabel;

    private float _fireballCooldown;
    private float _healCooldown;

    private void Awake()
    {
        _healthBar = uiDocument.rootVisualElement.Q<UnityEngine.UIElements.ProgressBar>("player-health");
        _targetHealthBar = uiDocument.rootVisualElement.Q<UnityEngine.UIElements.ProgressBar>("target-health");
        _fireballCooldownLabel = uiDocument.rootVisualElement.Q<Label>("fireball-cooldown");
        _healCooldownLabel = uiDocument.rootVisualElement.Q<Label>("heal-cooldown");

        _healthBar.highValue = maxPlayerHealth;
        _healthBar.value = maxPlayerHealth;
        
        _targetHealthBar.highValue = maxTargetHealth;
        _targetHealthBar.value = maxTargetHealth;

        RedrawCooldowns();
    }

    private void Update()
    {
        _healCooldown = _healCooldown - Time.deltaTime >= 0 ? _healCooldown - Time.deltaTime : 0;
        _fireballCooldown = _fireballCooldown - Time.deltaTime >= 0 ? _fireballCooldown - Time.deltaTime : 0;
        
        RedrawCooldowns();
    }
    
    public void SetHealth(float value)
    {
        _healthBar.value = value;
    }

    public void SetHealCooldown()
    {
        _healCooldown = healSpell.Cooldown;
    }

    public void SetFireballCooldown()
    {
        _fireballCooldown = fireballSpell.Cooldown;
    }

    public void SetTargetHealth(float value)
    {
        _targetHealthBar.value = value;
    }

    private void RedrawCooldowns()
    {
        _fireballCooldownLabel.text = $"Fireball: {_fireballCooldown}s";
        _healCooldownLabel.text = $"Heal: {_healCooldown}s";
    }
}