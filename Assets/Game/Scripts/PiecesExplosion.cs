using UnityEngine;

public class PiecesExplosion : MonoBehaviour
{
    public ParticleSystem ExplosionEffect;
    public Material[] materials;
	
    public void Trigger()
    {
        var particles = Instantiate(ExplosionEffect, transform.position, ExplosionEffect.transform.rotation);
        var renderer = particles.GetComponent<ParticleSystemRenderer>();
        renderer.materials = materials;
    }
}
