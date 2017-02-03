using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationText : MonoBehaviour
{
    Text main;
    string originalText;

    void Start()
    {
        main = GetComponent<Text>();
        originalText = main.text;
    }

    void Update ()
    {
        main.text = originalText.Replace("{0}", LevelManager.CurrentLevel.ToString());
    }
}
