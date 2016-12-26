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
    float vertical;

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
        Vector3 force = (Vector3.right * horizontal) + (Vector3.up * vertical);
        body.AddForce(force * Speed, ForceMode.Acceleration);
    }

    // Input update
    void Update ()
	{
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");

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
