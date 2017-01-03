using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	ProjectileLauncher launcher;

    public ParticleSystem DeathEffect;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;
    
    void Start()
	{
		launcher = GetComponent<ProjectileLauncher>();
    }

    void Update()
	{
		launcher.Launch(Vector3.forward * -1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            OnDeath.Invoke();
			EventManager.TriggerEvent (EventManager.Events.EnemyKilled);
            Instantiate(DeathEffect, transform.position, transform.rotation);
			Destroy (gameObject);
        }
    }
}
