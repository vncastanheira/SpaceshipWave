using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridAgent))]
public class GridInput : MonoBehaviour
{
    protected GridAgent agent;
    protected ProjectileLauncher projectileLauncher;

    protected float _timer;
    public float ProjectileDelay;

    public virtual void Start () {
        agent = GetComponent<GridAgent>();
        projectileLauncher = GetComponent<ProjectileLauncher>();
        _timer = ProjectileDelay;
    }

    private void Update()
    {
        if (Grid.instance.isPaused)
            return;

        GetInput();
    }

    public virtual void GetInput() { }
}
