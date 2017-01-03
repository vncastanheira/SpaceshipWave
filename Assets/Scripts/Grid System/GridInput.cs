using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridAgent))]
public class GridInput : MonoBehaviour
{
    protected GridAgent agent;

	public virtual void Start () {
        agent = GetComponent<GridAgent>();
	}

    private void Update()
    {
        GetInput();
    }

    public virtual void GetInput() { }
}
