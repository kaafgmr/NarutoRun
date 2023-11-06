using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    private float Health;
    public float MaxHealth;

    public UnityEvent<float> OnHurt;
    public UnityEvent<float> OnAddHealth; 
    public UnityEvent<float> SendMaxHealth;
    public UnityEvent OnDie;

    private void Awake()
    {
        Reset();
        SendMaxHealth.Invoke(Health);
    }

    public void GetHurt(float Damage)
    {
        Health -= Damage;
        OnHurt.Invoke(Health);
        if (Health <= 0)
        {
            OnDie.Invoke();
        }
    }

    public void AddHealth(float amount)
    {
        Health += amount;
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        OnAddHealth.Invoke(Health);
    }

    public void Reset()
    {
        Health = MaxHealth;
    }

    public float GetCurrentHealth()
    {
        return Health;
    }
}
