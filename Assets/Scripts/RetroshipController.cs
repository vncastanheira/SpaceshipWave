using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class RetroshipController : MonoBehaviour
{
	public float Speed;
	public Rigidbody Projectile;
	public Collider collider;

	Ray _ray;
	void Start () {
	
	}
	
	void Update ()
	{
		var vertical = CrossPlatformInputManager.GetAxis("Vertical");
		transform.Translate(Vector3.forward * vertical * Speed);

		var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		transform.Translate(Vector3.right * horizontal * Speed);

		_ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast(_ray, out hit))
		{
			transform.position = new Vector3(transform.position.x, hit.point.y + 1.0f, transform.position.z);
		}

		// Shooting
		if (CrossPlatformInputManager.GetButtonDown("Fire1"))
		{
			Instantiate(Projectile, transform.position, Projectile.transform.rotation);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(_ray);
	}
}
