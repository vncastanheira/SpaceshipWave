using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BarrierController : MonoBehaviour
{
    [Range(0, 100)]
    public int MaxHealth;
    public float RechargeTimer;

    [HideInInspector]
    public float Health;

    private float timer;
    bool isAlive;

    public UnityEvent Recharging;

    void Start ()
    {
        Health = MaxHealth;
        timer = RechargeTimer;
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            if (timer <= 0 && Health < MaxHealth)
            {
                Health += Time.deltaTime * 2;
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Recharging.Invoke();
                }
            }
        }
    }

    public void DamageShield()
    {
        Health--;
        timer = RechargeTimer;
        if (Health <= 0)
        {
            isAlive = false;
        }
    }
}
