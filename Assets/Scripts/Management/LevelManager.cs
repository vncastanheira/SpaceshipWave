using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public Level levelConfiguration;

    public UnityEvent OnWinning;
    public UnityEvent OnRestart;
    int enemyCounter = 0;

    // Use this for initialization
    void Start()
    {
        GenerateEnemies();
        Grid.instance.FindAgents();
        EventManager.StartListening(EventManager.Events.EnemyKilled, EnemyKilled);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && enemyCounter == 0)
        {
            GenerateEnemies();
            OnRestart.Invoke();
        }
    }

    void GenerateEnemies()
    {
        GameObject Enemies = new GameObject("Enemies");
        Enemies.transform.position = new Vector3(0, 0, 18);
        foreach (var placement in levelConfiguration.Enemies)
        {
            foreach (var location in placement.Locations)
            {
                var agent = Instantiate(placement.Enemy);
                agent.transform.SetParent(Enemies.transform);
                agent.GridPosition = location;
                agent.transform.tag = "Enemy";
            }
            enemyCounter += placement.Locations.Length;
        }
    }

    void EnemyKilled()
    {
        enemyCounter--;
        if (enemyCounter <= 0)
        {
            print("Congratulations! You won!");
            OnWinning.Invoke();
        }
    }
}
