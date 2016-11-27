using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;
using UnityEditor;

public class RetroshipController : MonoBehaviour
{
    [HideInInspector]
    public float Velocity;
	public float Speed;
    [Range(0, 100)]
    public int MaxHealth;
    [HideInInspector]
    public int Health;

    BarrierController barrier;
	Rigidbody body;
	ProjectileLauncher launcher;

    public UnityEvent ProjectileHit;

	void Start ()
	{
		body = GetComponent<Rigidbody>();
		launcher = GetComponent<ProjectileLauncher>();
        barrier = GetComponent<BarrierController>();

        Health = MaxHealth;
	}
	
	void Update ()
	{
        Velocity = CrossPlatformInputManager.GetAxis("Horizontal");
		body.AddForce(Vector3.right * Velocity * Speed, ForceMode.Acceleration);

		// Shooting
		if (CrossPlatformInputManager.GetButton("Fire1"))
		{
			launcher.Launch(Vector3.forward);
		}
    }

	void OnCollisionEnter(Collision collision)
	{
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (barrier.Health > 0)
            {
                barrier.DamageShield();
            }
            else
            {
                Health--;
                ProjectileHit.Invoke();
            }

        }
    }
}
