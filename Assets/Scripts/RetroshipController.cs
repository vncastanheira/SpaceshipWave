using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class RetroshipController : MonoBehaviour
{
	public Transform Spaceship;

	public float Speed;
	public Rigidbody Projectile;

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
			//float angleInRadians = Mathf.Acos(Mathf.Clamp(hit.normal.y, -1f, 1f));
			//float angleInRadians = Mathf.Acos(hit.normal.y);
			//SpaceshipParent.rotation = Quaternion.AngleAxis(angleInRadians * Mathf.Rad2Deg, Vector3.forward);
			float angle = Vector3.Angle(Vector3.up, hit.normal);
			var cross = Vector3.Cross(Vector3.up, hit.normal);
			if (cross.z < 0) angle = -angle;
			Spaceship.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
