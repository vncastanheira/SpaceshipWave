using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public abstract class PlayerInput : MonoBehaviour
{
    protected float horizontal;
    protected float vertical;

    public abstract void GetInput();
    public abstract void Run();
	
}
