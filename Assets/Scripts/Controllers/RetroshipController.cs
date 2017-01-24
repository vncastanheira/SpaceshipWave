﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;

public class RetroshipController : MonoBehaviour
{
    // Ship status
	public float Speed;
    
    [Range(0, 100)]
    public int MaxHealth;
    
    [HideInInspector]
    public int Health;
    
    [HideInInspector]
    public bool IsAlive 
    {
        get { return Health > 0; }
    }

    [HideInInspector]
    public bool HasBarrier
    {
        get
        {
            return barrier.Health > 0;
        }
    }

    // Components
    [HideInInspector]
    BarrierController barrier;
    [HideInInspector]
    Rigidbody body;

    // Ship events
    public UnityEvent ProjectileHit;
    public UnityEvent CriticalHealth;
    public UnityEvent Shooting;
    public UnityEvent OnDeath;

    // Ship conditions
    bool _critical = false;

	void Start ()
	{
        barrier = GetComponent<BarrierController>();
        body = GetComponent<Rigidbody>();
        
        Health = MaxHealth;
        OnDeath.AddListener(() => {
            body.velocity = Vector3.zero;
        });
	}

    // Physics-related update
    void FixedUpdate()
    {
        if(!IsAlive)
            return;
    }

    // Input update
    void Update ()
	{
        if(!IsAlive)
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Projectile"))
        {
            if(!IsAlive)
                return;

            ProjectileHit.Invoke();
            if (HasBarrier)
            {
                barrier.DamageShield();
            }
            else
            {
                Health--;
                if(Health == 0)
                    OnDeath.Invoke();
            }

            if ((Health < (MaxHealth/2)) && !_critical)
            {
                CriticalHealth.Invoke();
                _critical = true;
            }
        }
    }
}
