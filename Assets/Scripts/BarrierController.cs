using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BarrierController : MonoBehaviour
{

    [Range(0, 100)]
    public int MaxHealth;
    [HideInInspector]
    public int Health;

    public Transform Barrier;

    void Start () {

        Health = MaxHealth;
    }

    public void DamageShield()
    {
        Health--;
        if (Health <= 0)
        {
            Barrier.transform.Translate(Vector3.up * 10);
        }
    }
}
