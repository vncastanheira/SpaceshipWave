using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update () {
        transform.Rotate(Vector3.up, 0.1f);
	}
}
