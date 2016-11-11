using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class RetroshipController : MonoBehaviour
{
	public float Speed;
	Rigidbody body;
	ProjectileLauncher launcher;


	Ray _ray;
	void Start ()
	{
		body = GetComponent<Rigidbody>();
		launcher = GetComponent<ProjectileLauncher>();
	}
	
	void Update ()
	{
		//var vertical = CrossPlatformInputManager.GetAxis("Vertical");
		var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		body.AddForce(Vector3.right * horizontal * Speed, ForceMode.Acceleration);

		//body.AddForce(Vector3.forward * vertical * Speed);
		// Shooting
		if (CrossPlatformInputManager.GetButton("Fire1"))
		{
			launcher.Launch(Vector3.forward);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_ray);
	}
}
