using System;
using SaiUtils.GameEvents;
using SaiUtils.ScriptableVariables;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] bool _useScriptableVariables = false;
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currentHealth = 100f;
    [SerializeField] FloatVariable _maxHealthVariable;
    [SerializeField] FloatVariable _currentHealthVariable;

    [Header("Feedback")]
    [SerializeField] GameObject _healingFeedbackPrefab;
    [SerializeField] GameObject _damageFeedbackPrefab;
    [SerializeField] GameObject _deathFeedbackPrefab;

    [Header("Events")]
    [SerializeField] FloatEvent _onHealthChanged;

    public float MaxHealth => _useScriptableVariables ? _maxHealthVariable.Value : _maxHealth;
    public float CurrentHealth => _useScriptableVariables ? _currentHealthVariable.Value : _currentHealth;

    public void ChangeHealth(float amount)
    {
        if (_useScriptableVariables) _currentHealthVariable.Value += amount;
        else _currentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            if (_useScriptableVariables) _currentHealthVariable.Value = MaxHealth;
            else _currentHealth = MaxHealth;
        }

        if (CurrentHealth <= 0) Die();

        if (amount > 0)
        {
            if (_healingFeedbackPrefab != null)
            {
                Instantiate(_healingFeedbackPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (_damageFeedbackPrefab != null)
            {
                Instantiate(_damageFeedbackPrefab, transform.position, Quaternion.identity);
            }
        }

        _onHealthChanged?.Raise(CurrentHealth / MaxHealth);
    }

    

    public void Die()
    {
        if (_deathFeedbackPrefab != null)
        {
            Instantiate(_deathFeedbackPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}