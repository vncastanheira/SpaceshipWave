using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	ProjectileLauncher launcher;
    Rigidbody body;

    public ParticleSystem DeathEffect;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;
    
    void Awake()
	{
		launcher = GetComponent<ProjectileLauncher>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
	{
		launcher.Launch(Vector3.forward * -1);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Instantiate(DeathEffect, transform.position, transform.rotation);
        }
    }
}
