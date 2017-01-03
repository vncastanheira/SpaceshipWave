using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerAnimation))]
public class GridArrowsInput : GridInput
{
    PlayerAnimation anim;

    float horizontal;
    float vertical;
    
    public override void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        base.Start();
    }

    public override void GetInput()
    {
        horizontal = vertical = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            vertical -= 1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            vertical += 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            horizontal -= 1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            horizontal += 1;

        agent.Move(new Vector2(horizontal, vertical));

        anim.SetVelocity(agent.Direction.x * 30);

        base.GetInput();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 50, 200, 50), "Horizontal: " + horizontal);
    }
}
