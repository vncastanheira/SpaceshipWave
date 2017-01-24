using UnityEngine;
using System.Collections;

public class PropRotation : MonoBehaviour {

    public float angle;
    public Vector3 Axis;

    void Update ()
    {
        if (Grid.instance.isPaused)
            return;

        transform.Rotate(Axis, angle);
	}
}
