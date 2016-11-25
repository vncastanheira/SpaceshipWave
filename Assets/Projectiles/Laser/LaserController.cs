﻿using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
	public float Force;
	public float Lifetime;
	public ParticleSystem particle;
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
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Instantiate(particle, other.transform.position, particle.transform.rotation);
            Destroy(gameObject);
        }
    }
}
