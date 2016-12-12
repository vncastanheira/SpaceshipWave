using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;
using UnityEditor;

public class RetroshipController : MonoBehaviour
{
    // Ship status
	public float Speed;
    [Range(0, 100)]
    public int MaxHealth;
    [HideInInspector]
    public int Health;
    [HideInInspector]
    public bool HasBarrier
    {
        get
        {
            return barrier.Health > 0;
        }
    }
    float horizontal;

    // Components
    [HideInInspector]
	public Rigidbody body;
    BarrierController barrier;
	ProjectileLauncher launcher;

    // Ship events
    public UnityEvent ProjectileHit;
    public UnityEvent CriticalHealth;
    public UnityEvent Shooting;

    // Ship conditions
    bool _critical = false;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
		launcher = GetComponent<ProjectileLauncher>();
        barrier = GetComponent<BarrierController>();

        Health = MaxHealth;
	}

    // Physics-related update
    void FixedUpdate()
    {
        body.AddForce(Vector3.right * horizontal * Speed, ForceMode.Acceleration);
    }

    // Input update
    void Update ()
	{
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

		// Shooting
		if (CrossPlatformInputManager.GetButton("Fire1"))
		{
    		launcher.Launch(Vector3.forward);
            Shooting.Invoke();
        }
    }

	void OnCollisionEnter(Collision collision)
	{
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            ProjectileHit.Invoke();

            if (HasBarrier)
            {
                barrier.DamageShield();
            }
            else
            {
                Health--;
            }

            if ((Health < (MaxHealth/2)) && !_critical)
            {
                CriticalHealth.Invoke();
                _critical = true;
            }
        }
    }
}
