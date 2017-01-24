using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
	[SerializeField]
	private Transform Cannon;
	[SerializeField]
	private GameObject Projectile;

    public float Speed;

    public void Launch(Vector3 direction)
	{
        var laser = Instantiate(Projectile);
        laser.transform.position = Cannon.position;
        var laserController = laser.GetComponent<LaserController>();
        laserController.Launch(direction * Speed);
    }
}
