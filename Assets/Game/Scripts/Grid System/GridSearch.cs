using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Obsolete]
public static class GridSearch
{
    public static List<Vector2> Search(Vector2 origin, Vector2 target)
    {
        var frontier = new Queue<Vector2>();
        frontier.Enqueue(origin);

        var visited = new HashSet<Vector2>();
        visited.Add(origin);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            var neighbors = Grid.instance.Neighbors(current).ToList();
            foreach (var next in neighbors)
            {
                if (!visited.Contains(next))
                {
                    frontier.Enqueue(next);
                    visited.Add(next);
                }
            }
        }
        return visited.ToList();
    }
}
