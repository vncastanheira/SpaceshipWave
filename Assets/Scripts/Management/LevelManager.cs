using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	int enemyCounter = 0;

	// Use this for initialization
	void Start () {
		enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
		EventManager.StartListening(EventManager.Events.EnemyKilled, EnemyKilled);
		print (enemyCounter + " enemies found");
	}
	
	void EnemyKilled() {
		enemyCounter--;
		if (enemyCounter <= 0)
		{
			print("Congratulations! You won!");
		}
	}
}
