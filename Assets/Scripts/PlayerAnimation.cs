using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RetroshipController))]
public class PlayerAnimation : MonoBehaviour {

    public Animator ShipAnimator;
    public Animator CanvasAnimator;
    
	public void SetVelocity (float velocity)
    {
        ShipAnimator.SetFloat("velocity", velocity);
    }
    
    public void Shake()
    {
        CanvasAnimator.SetTrigger("hit");
    }
}
