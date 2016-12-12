using UnityEngine;
using System.Collections;
using UnityEditor;

public class LaserController : MonoBehaviour
{
	public float Force;
	public float Lifetime;
	Rigidbody rigidBody;

	void Awake () {
		rigidBody = GetComponent<Rigidbody>();
	}

	public void Launch(Vector3 direction)
	{
		rigidBody.AddForce(direction * Force);

		Destroy(gameObject, Lifetime);
	}

	void OnCollisionEnter(Collision collision)
	{
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
            case "Player":
            case "Barrier":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
