using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAI_Static : GridInput
{
    public override void GetInput()
    {
        base.GetInput();
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            projectileLauncher.Launch(transform.forward);
            _timer = ProjectileDelay;
        }

    }
}
