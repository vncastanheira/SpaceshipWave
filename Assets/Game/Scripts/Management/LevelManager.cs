using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _levelManager;
    public static LevelManager instance
    {
        get
        {
            if (!_levelManager)
            {
                _levelManager = FindObjectOfType(typeof(LevelManager)) as LevelManager;

                if (!_levelManager)
                {
                    Debug.LogError("There needs to be one active LevelManager script on a GameObject in your scene.");
                    return null;
                }
            }

            return _levelManager;
        }
    }

    public int levelOrder = 0;
    public static int CurrentLevel { get { return instance.levelOrder; } }

    public Level[] Levels;

    public UnityEvent OnStart;
    public UnityEvent OnWinning;
    public UnityEvent OnRestart;
    public UnityEvent OnNextLevel;
    int enemyCounter = 0;

    // Use this for initialization
    void Start()
    {
        GenerateEnemies();
        Grid.instance.FindAgents();
        EventManager.StartListening(EventManager.Events.EnemyKilled, EnemyKilled);

        OnStart.AddListener(Grid.Pause);
        OnRestart.AddListener(Grid.Pause);
        OnNextLevel.AddListener(Grid.Pause);
        
        OnStart.Invoke();
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
            levelOrder++;
            if (levelOrder >= Levels.Length)
            {
                Debug.Log("No more levels");
                levelOrder--;
                return;
            }

            GenerateEnemies();
            Grid.instance.FindAgents();
            OnNextLevel.Invoke();
        }
    }

    void GenerateEnemies()
    {
        GameObject Enemies = new GameObject("Enemies");
        Enemies.transform.position = new Vector3(0, 0, 18);
        foreach (var placement in Levels[levelOrder].Enemies)
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
