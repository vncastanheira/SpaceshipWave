using UnityEngine;
using System.Collections;

public class PropRotation : MonoBehaviour {

    public float angle;
    public Vector3 Axis;

    void Update ()
    {
        transform.Rotate(Axis, angle);
	}
}
