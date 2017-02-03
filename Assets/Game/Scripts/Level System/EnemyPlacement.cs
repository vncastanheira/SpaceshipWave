using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceshipWave/Enemy Placement")]
public class EnemyPlacement : ScriptableObject
{
    public GridAgent Enemy;
    public Vector2[] Locations;
}
