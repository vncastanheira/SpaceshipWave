using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXT_FLASH : MonoBehaviour {

	Text text;
	AudioSource audio;
	bool triggered;
	int enemyCounter = 0;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		audio = GetComponent<AudioSource> ();
		enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
		EventManager.StartListening(EventManager.Events.EnemyKilled, Trigger);
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered) {
			text.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));
		}
	}

	void Trigger() {
		enemyCounter--;
		if (enemyCounter <= 0)
		{
			print("Congratulations! You won!");
			text.text = "YOU WON";
			audio.Play ();
			triggered = true;
		}
	}
}
