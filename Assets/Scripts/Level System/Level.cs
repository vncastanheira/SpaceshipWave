using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceshipWave/Level")]
public class Level : ScriptableObject
{
    [TextArea]
    public string Description;
    public EnemyPlacement[] Enemies;
}
