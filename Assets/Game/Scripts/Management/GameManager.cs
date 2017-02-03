using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// When debug mode is activated, the manager
    /// won't load the Menu by default
    /// </summary>
    public bool DebugMode;

    public string[] Scenes;

    int currentSceneIndex;
    string currentSceneName;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        currentSceneIndex = 0;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        if (!DebugMode)
        {
            EventManager.StartListening(EventManager.Events.NewGame, NewGame);
            EventManager.StartListening(EventManager.Events.NextLevel, NextLevel);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NewGame()
    {
        var scene = Scenes.FirstOrDefault();
        if (scene == null)
        {
            Debug.LogError("No scenes available");
        }
        else
        {
            currentSceneName = scene;
            SceneManager.LoadScene(scene);
        }
    }

    public void NextLevel()
    {
        var nextScene = currentSceneIndex++;
        var scene = Scenes.ElementAtOrDefault(nextScene);
        if (scene == null)
        {
            Debug.LogError("Next scene (" + nextScene + ") is not available");
        }
        else
        {
            currentSceneName = scene;

            SceneManager.UnloadSceneAsync(currentSceneName);
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }

    IEnumerator AsyncLoad(string scene)
    {
        var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                yield break;
            }
        }

        yield return null;

    }

    void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 400, 50), "ESC to exit | R to restart");
    }
}
