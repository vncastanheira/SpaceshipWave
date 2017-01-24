using UnityEngine;
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
    
    public void HitHull(Collider other)
    {
        if (other.CompareTag("Player Projectile"))
        {
            OnDeath.Invoke();
			EventManager.TriggerEvent (EventManager.Events.EnemyKilled);
            Instantiate(DeathEffect, transform.position, transform.rotation);
			Destroy (gameObject);
        }
    }
}
