using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Grid system
/// </summary>
public class Grid : MonoBehaviour
{
    // Distance from each cell, in each dimensions in the world coordinates
    public Vector2 CellSize;
    // Maximum size of the grid
    public Vector2 GridDimension;
    // All agents in the scene
    GridAgent[] Agents;

    private static Grid _grid;
    public static Grid instance
    {
        get
        {
            if (!_grid)
            {
                _grid = FindObjectOfType(typeof(Grid)) as Grid;

                if (!_grid)
                {
                    Debug.LogError("There needs to be one active Grid script on a GameObject in your scene.");
                    return null;
                }
            }

            return _grid;
        }
    }

    // Pauses the agents and inputs
    [HideInInspector]
    public bool isPaused = false;
    public UnityEvent OnPause;
    public UnityEvent OnResume;

    public static void Pause() { instance.isPaused = true; instance.OnPause.Invoke(); }
    public static void Resume() { instance.isPaused = false; instance.OnResume.Invoke(); }
    public static void PauseToggle()
    {
        instance.isPaused = !instance.isPaused;
        if (instance.isPaused)
        {
            instance.OnPause.Invoke();
        }
        else
        {
            instance.OnResume.Invoke();
        }
    }

    // TODO: move to a Game Manager
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseToggle();
        }
    }


    public void FindAgents()
    {
        Agents = FindObjectsOfType<GridAgent>();
    }

    /// <summary>
    /// Check which positions around the current is not outside boundaries
    /// </summary>
    /// <param name="current">The center position</param>
    /// <returns>A list with all positions not outside the boundaries</returns>
    public IEnumerable<Vector2> Neighbors(Vector2 current)
    {
        Vector2[] Directions =
        {
            Vector2.left,
            Vector2.right,
            Vector2.up,
            Vector2.down
        };

        return Directions.Select(d => d + current);
    }

    // Approximates a irregular position to a valid grid position
    public static Vector3 Snap(Vector3 position)
    {
        var cell = instance.CellSize;

        var x = Mathf.Pow(cell.x, Mathf.RoundToInt(Mathf.Log(position.x, cell.x)));
        var y = Mathf.Pow(cell.y, Mathf.RoundToInt(Mathf.Log(position.y, cell.y)));
        return new Vector3(x, y, position.z);
    }

    /// <summary>
    /// Limit the position in the Grid space (ignores transform)
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector2 LimitPosition(Vector2 position)
    {
        var limit = instance.GridDimension;
        var x = Mathf.Clamp(position.x, 0, limit.x);
        var y = Mathf.Clamp(position.y, 0, limit.y);
        return new Vector2(x, y);
    }

    public Vector2 GetCellSize()
    {
        return instance.CellSize;
    }

    /// <summary>
    /// Check if a position is not outside the boundaries
    /// and is not occupied by another agent
    /// </summary>
    /// <param name="pos">Check position</param>
    /// <returns>If position is valid and unnocupied</returns>
    public bool IsValidPosition(Vector2 pos, bool ignorePlayer = false)
    {
        Vector3 target = Vector3.Scale(pos, CellSize);
        target.z = 18;
        
        return !IsOccupied(pos, ignorePlayer)
            && (pos.x >= 0 && pos.x <= GridDimension.x)
            && (pos.y >= 0 && pos.y <= GridDimension.y);
    }

    public bool IsOccupied(Vector2 pos, bool ignorePlayer)
    {
        if (Agents != null)
        {
            var agent = Agents.FirstOrDefault(a => a.GridPosition.Equals(pos));
            if (agent == null)
            {
                return false;
            }
            else if (agent.CompareTag("Player") && ignorePlayer)
            {
                return false;
            }

            return true;
        }
        return false;
    }
}