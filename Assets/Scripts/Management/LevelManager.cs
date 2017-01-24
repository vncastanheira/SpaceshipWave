using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int LevelOrder = 0;
    public Level[] Levels;

    public UnityEvent OnWinning;
    public UnityEvent OnRestart;
    int enemyCounter = 0;

    // Use this for initialization
    void Start()
    {
        GenerateEnemies();
        Grid.instance.FindAgents();
        EventManager.StartListening(EventManager.Events.EnemyKilled, EnemyKilled);
        Grid.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && enemyCounter == 0)
        {
            GenerateEnemies();
            Grid.instance.FindAgents();
            OnRestart.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && enemyCounter == 0)
        {
            LevelOrder++;
            if (LevelOrder >= Levels.Length)
            {
                Debug.Log("No more levels");
                LevelOrder--;
                return;
            }

            GenerateEnemies();
            Grid.instance.FindAgents();
            OnRestart.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Grid.PauseToggle();
        }
    }

    void GenerateEnemies()
    {
        GameObject Enemies = new GameObject("Enemies");
        Enemies.transform.position = new Vector3(0, 0, 18);
        foreach (var placement in Levels[LevelOrder].Enemies)
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

    private void OnGUI()
    {
        string message = "Game is: " + (Grid.instance.isPaused ? "OFF" : "ON");
        GUI.TextField(new Rect(0, 0, 300, 50), message);
    }
}
