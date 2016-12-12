using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RetroshipController))]
public class PlayerAnimation : MonoBehaviour {

    public Animator ShipAnimator;
    public Animator CanvasAnimator;
    RetroshipController _controller;

    void Start ()
    {
        _controller = GetComponent<RetroshipController>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShipAnimator.SetFloat("velocity", _controller.body.velocity.x);
    }

    public void Shake()
    {
        CanvasAnimator.SetTrigger("hit");
    }
}
