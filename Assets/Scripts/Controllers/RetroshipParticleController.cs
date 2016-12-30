using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetroshipParticleController : MonoBehaviour
{
    public ParticleSystem Shield;
    AudioSource ShieldAudio;
    public ParticleSystem Hull;
    AudioSource HullAudio;

    RetroshipController MainController;

    void Start()
    {
        MainController = GetComponent<RetroshipController>();
        ShieldAudio = Shield.GetComponent<AudioSource>();
        HullAudio = Hull.GetComponent<AudioSource>();
    }

    public void Emit()
    {
        if (MainController.HasBarrier)
        {
            Shield.Emit(1);
            ShieldAudio.Play();
        }
        else
        {
            Hull.Play(withChildren: true);
            HullAudio.Play();
        }
    }
}
