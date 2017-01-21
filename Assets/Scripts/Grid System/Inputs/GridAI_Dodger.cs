using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Enemy AI.
/// Dodges player projectiles.
/// </summary>
public class GridAI_Dodger : GridInput
{
    // This is a delay between each time the enemy will dodge 
    // the player's projectiles. Otherwise, it would be impossible
    // to hit it
    public float ColldownMaxTimer = 0;
    public float cooldownTimer = 0;

    // Enemy only can move on a Horizontal or Vertical Axis
    Vector2[] Directions =
    {
        Vector2.left,
        Vector2.right,
        Vector2.up,
        Vector2.down
    };

    void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    // Expected to be triggered by a SendMessage when
    // a player projectile hit a detection area
    public void PlayerFireDetected(Collider other)
    {
        if (other.CompareTag("Player Projectile"))
        {
            if (cooldownTimer > 0)
                return;

            // shuffle
            Vector2[] Pool = Directions.ToArray();
            for (int i = 0; i < 100; i++)
            {
                int aIndex = Random.Range(0, Directions.Length);
                int bIndex = Random.Range(0, Directions.Length);
                var temp = Pool[aIndex];
                Pool[aIndex] = Pool[bIndex];
                Pool[bIndex] = temp;
            }
            foreach (var pos in Pool)
            {
                if (Grid.instance.IsValidPosition(agent.GridPosition + pos))
                {
                    agent.Move(pos);
                    cooldownTimer = ColldownMaxTimer;
                    return;
                }
            }
        }
    }
}
