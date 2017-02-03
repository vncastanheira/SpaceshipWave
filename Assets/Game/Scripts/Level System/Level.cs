using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceshipWave/Level")]
public class Level : ScriptableObject
{
    [TextArea]
    public string Description;
    public EnemyPlacement[] Enemies;

    // Distance from each cell, in each dimensions in the world coordinates
    public Vector2 CellSize;
    // Maximum size of the grid
    public Vector2 GridDimension;
}
