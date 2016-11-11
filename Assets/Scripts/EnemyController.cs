using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	ProjectileLauncher launcher;

	void Awake()
	{
		launcher = GetComponent<ProjectileLauncher>();
	}

	void Update()
	{
		launcher.Launch(Vector3.forward * -1);
	}
}
