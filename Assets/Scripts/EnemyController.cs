using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	ProjectileLauncher launcher;
    Rigidbody body;



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
        }
    }
}
