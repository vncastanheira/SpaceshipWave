﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridAI_Seeker : GridInput
{
    GridAgent Player;
    List<Vector2> Path;

    public override void Start()
    {
        var player = FindObjectOfType<RetroshipController>();
        Player = player.GetComponent<GridAgent>();

        Path = new List<Vector2>();
        base.Start();
    }

    public override void GetInput()
    {
        if (agent.OnDestination && !agent.GridPosition.Equals(Player.GridPosition))
        {
            var neighboors = Grid.instance.Neighbors(agent.GridPosition);
            foreach (var pos in neighboors.OrderBy(p => Vector2.Distance(p, Player.GridPosition)))
            {
                if (Grid.instance.IsValidPosition(pos,ignorePlayer: true))
                {
                    agent.Move(pos - agent.GridPosition);
                    break;
                }
            }

        }

        base.GetInput();
    }

    private void OnDrawGizmos()
    {
        var cellSize = Grid.instance.CellSize;
        Vector2 from = Path.FirstOrDefault();
        if (from == null)
            return;

        Gizmos.color = Color.red;
        for (int i = 1; i < Path.Count; i++)
        {
            var target = Path[i];
            Gizmos.DrawLine(Vector2.Scale(from, cellSize), Vector2.Scale(target, cellSize));
            from = Path[i];
        }
    }
}