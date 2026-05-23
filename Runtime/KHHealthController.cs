using System;
using System.Collections.Generic;
using KH;
using UnityEngine;

public sealed class KHHealthController : MonoBehaviour
{
    #region FIELDS █████████████████████████████████████████████████████████████████████████████████████████████

    private readonly List<Action> _onDeathListeners = new();
    private readonly List<Action> _onReviveListeners = new();
    private readonly List<Action> _onHealthIncreaseListeners = new();
    private readonly List<Action> _onHealthDecreaseListeners = new();
    private readonly List<Action> _onMaxHealthListeners = new();

    private float _lastDamageTime = -1f;
    private int _maxHealth = 1;
    private float _health = 1;
    private bool _isDead;

    public bool IsMaxHealth => _health >= _maxHealth;
    public bool IsDead
    {
        get => _isDead;
        private set
        {
            if (_isDead == value)
                return;
                
            _isDead = value;

            if (_isDead)
                OnDeath();
            else
                OnRevive();
        }
    }
    public float Health
    {
        get => _health;
        private set
        {
            if (_health == value)
                return;

            if (_health < value)
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
                OnHealthIncrease();
            }
            else if (_health > value)
            {
                _health = Mathf.Clamp(value, 0, _maxHealth);
                OnHealthDecrease();
            }

            if (IsMaxHealth)
                OnMaxHealth();

            IsDead = _health <= 0;
        }
    }

    #region SERIALIZABLE FIELDS █ █ █

    [SerializeField] private string _showHealth;

    #endregion
    #endregion
    #region UNITY EVENT FUNCTIONS ██████████████████████████████████████████████████████████████████████████████

#if UNITY_EDITOR
    void Start()
    {
        _showHealth = $"{_health} / {_maxHealth}  MAX HEALTH";
    }
#endif

    #endregion
    #region PRIVATE METHODS ████████████████████████████████████████████████████████████████████████████████████

    private static void AddListener(List<Action> list, Action listener)
    {
        if (listener == null || list.Contains(listener))
            return;

        list.Add(listener);
    }

    private static void RemoveListener(List<Action> list, Action listener)
    {
        if (listener == null)
            return;

        list.Remove(listener);
    }

    private static void Notify(List<Action> list)
    {
        // Make a copy to safely iterate
        var listenersCopy = list.ToArray();

        listenersCopy.KHForEachElement(listener =>
        {
            try { listener?.Invoke(); }
            catch (Exception e) { Debug.LogError($"Listener threw exception: {e}"); }
        });
    }

    public void AddOnDeathListener(Action listener) => AddListener(_onDeathListeners, listener);
    public void RemoveOnDeathListener(Action listener) => RemoveListener(_onDeathListeners, listener);

    public void AddOnReviveListener(Action listener) => AddListener(_onReviveListeners, listener);
    public void RemoveOnReviveListener(Action listener) => RemoveListener(_onReviveListeners, listener);

    public void AddOnHealthIncreaseListener(Action listener) => AddListener(_onHealthIncreaseListeners, listener);
    public void RemoveOnHealthIncreaseListener(Action listener) => RemoveListener(_onHealthIncreaseListeners, listener);

    public void AddOnHealthDecreaseListener(Action listener) => AddListener(_onHealthDecreaseListeners, listener);
    public void RemoveOnHealthDecreaseListener(Action listener) => RemoveListener(_onHealthDecreaseListeners, listener);

    public void AddOnMaxHealthListener(Action listener) => AddListener(_onMaxHealthListeners, listener);
    public void RemoveOnMaxHealthListener(Action listener) => RemoveListener(_onMaxHealthListeners, listener);

    private void OnDeath()
    {
        Notify(_onDeathListeners);
        _showHealth = "DEAD";
    }

    private void OnRevive()
    {
        Notify(_onReviveListeners);
        _showHealth = $"{_health} / {_maxHealth}  MAX HEALTH";
    }

    private void OnHealthIncrease()
    {
        _showHealth = $"{_health} / {_maxHealth}  healed";
        Notify(_onHealthIncreaseListeners);
    }

    private void OnHealthDecrease()
    {
        _lastDamageTime = Time.time;

        _showHealth = $"{_health} / {_maxHealth}. damaged";
        Notify(_onHealthDecreaseListeners);
    }

    private void OnMaxHealth()
    {
        Notify(_onMaxHealthListeners);
        _showHealth = $"{_health} / {_maxHealth}  FULL";
    }

    #endregion
    #region PUBLIC METHODS █████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// Returns how many seconds have passed since the entity last took damage.
    /// <para>Returns Mathf.Infinity if no damage was ever taken.</para>
    /// </summary>
    public float GetTimeSinceLastDamage()
    {
        if (_lastDamageTime < 0f)
            return Mathf.Infinity;

        return Time.time - _lastDamageTime;
    }

    /// <summary>
    /// Initializes the health system by setting the maximum health value
    /// and fully restoring the current health to maximum.
    /// </summary>
    /// <param name="newMaxHealth">The maximum amount of health this entity can have.</param>
    /// <param name="restoreHealth">Fully restores the entity's health to maximum.</param>
    /// <param name="triggerListeners">Trigger OnRevive, OnHealthIncrease, or OnMaxHealth.</param>
    public void SetMaxHealth(int newMaxHealth)
        => _maxHealth = newMaxHealth;

    public int GetMaxHealth()
        => _maxHealth;

    /// <summary>
    /// Reduces the entity's current health by the specified amount.
    /// <para>Trigger OnDeath logic when health reaches zero.</para>
    /// </summary>
    /// <param name="amount">The amount of health to remove.</param>
    public void RemoveHealth(float amount)
    {
        if (IsDead)
            return;

        Health -= amount;
    }

    /// <summary>
    /// Instantly reduces the entity's current health to zero, triggering OnDeath logic.
    /// </summary>
    public void RemoveFullHealth()
    {
        if (IsDead)
            return;

        Health = 0f;
    }

    /// <summary>
    /// Restores health based on a percentage of the entity's maximum health.
    /// <br/>
    /// The <paramref name="percent"/> value is clamped between 0 and 1.
    /// <br/>
    /// For example: 0.5f restores 50% of <see cref="_maxHealth"/>.
    /// </summary>
    /// <param name="percent">
    /// A value between 0 and 1 representing the fraction of max health to restore.
    /// </param>
    public void AddHealthPercentage(float percent)
    {
        if (IsMaxHealth || IsDead)
            return;

        Health += _maxHealth * Mathf.Clamp01(percent);
    }

    /// <summary>
    /// Increases the entity's health by the specified amount, up to the maximum health limit.
    /// </summary>
    /// <param name="amount">The amount of health to add.</param>
    public void RestoreHealth(float amount)
    {
        if (IsMaxHealth || IsDead)
            return;

        Health += amount;
    }

    /// <summary>
    /// Fully restores the entity's health to maximum.
    /// </summary>
    public void RestoreFullHealth()
    {
        if (IsMaxHealth || IsDead)
            return;

        Health = _maxHealth;
    }

    /// <summary>
    /// Revives the entity if it is dead, restoring it to active status and adding health.
    /// <para>Triggers OnRevive logic.</para>
    /// </summary>
    public void Revive()
    {
        if (!IsDead)
            return;

        Health = _maxHealth;
    }

    #endregion
}
