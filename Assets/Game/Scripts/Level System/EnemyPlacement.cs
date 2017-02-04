using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceshipWave/Enemy Placement")]
public class EnemyPlacement : ScriptableObject
{
    public GridAgent Enemy;
    public Vector2[] Locations;

    public bool AtLocation(Vector2 location)
    {
        if (Locations == null)
        {
            Debug.LogError("'Locations' array is empty or null");
            return false;
        }

        return Locations.SingleOrDefault(l => l == location) != null;
    }
}
