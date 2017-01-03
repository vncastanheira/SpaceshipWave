using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(RetroshipController))]
public class PlayerInput_Physics : PlayerInput
{
    [HideInInspector]
    public Rigidbody body;
    RetroshipController controller;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        controller = GetComponent<RetroshipController>();
    }

    public override void GetInput()
    {
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");
    }

    public override void Run()
    {
        Vector3 force = (Vector3.right * horizontal) + (Vector3.up * vertical);
        body.AddForce(force * controller.Speed, ForceMode.Acceleration);
    }
}
