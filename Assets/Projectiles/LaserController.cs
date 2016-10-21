using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
	public float Force;
	public float Lifetime;
	Rigidbody rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.AddForce(Vector3.forward * Force);

		Destroy(gameObject, Lifetime);
	}
	
}
