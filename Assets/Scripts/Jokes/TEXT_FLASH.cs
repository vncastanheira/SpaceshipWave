using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXT_FLASH : MonoBehaviour
{
	Text mainText;
	AudioSource audio;
	bool triggered;

	// Use this for initialization
	void Start () {
		mainText = GetComponent<Text> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			mainText.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		}
	}

	public void Trigger() {
        mainText.text = "Press \"SPACE\" to continue";
        audio.Play();
        triggered = true;
    }

    public void UnTrigger()
    {
        mainText.text = string.Empty;
        audio.Stop();
        triggered = false;
    }
}
