using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridAgent), true)]
public class GridAgentEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var agent = (GridAgent)target;
        GUILayout.BeginVertical();
        GUILayout.Label("Grid: " + agent.GridPosition.x + ", " + agent.GridPosition.y);
        GUILayout.EndVertical();

    }
}
