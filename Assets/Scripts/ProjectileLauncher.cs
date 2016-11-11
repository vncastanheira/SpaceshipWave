using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ProjectileLauncher : MonoBehaviour {

	[SerializeField]
	private float Delay;
	[SerializeField]
	private Transform Cannon;
	[SerializeField]
	private GameObject Projectile;

	float _timer;

	void Awake()
	{
		_timer = Delay;
	}

	void Update()
	{
		_timer -= Time.deltaTime;
	}

	public void Launch(Vector3 direction)
	{
		if (_timer <= 0)
		{
			var laser = Instantiate(Projectile);
			laser.transform.position = Cannon.position;
			var laserController = laser.GetComponent<LaserController>();
			laserController.Launch(direction);
			_timer = Delay;
		}
	}
}
