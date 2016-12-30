using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HELLO_DARKNESS : MonoBehaviour 
{
	public Material material;
	public MeshExplosion explosion;
	public ParticleSystem [] particles;

	AudioSource audioSource;

	[HideInInspector]
	public bool DarknessOn = false; 

	public void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void ActivateDarkness() 
	{
		if(!DarknessOn)
		{
			DarknessOn = true;
			foreach(var p in particles)
				p.Stop(true);
			Time.timeScale = 0.4f;
			audioSource.Play();
			explosion.Explode();
			Camera.main.transform.localPosition = new Vector3(0, 5, -10);
		}
	}


	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if(DarknessOn)
			Graphics.Blit(source, destination, material);
		else
			Graphics.Blit(source, destination);
	}
}
