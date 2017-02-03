using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RetroshipController))]
public class InterfaceController : MonoBehaviour
{
    RetroshipController _retroship;
    BarrierController _barrier;

    public Slider HealthSlider;
    public Slider BarrierSlider;

    void Start ()
    {
        _retroship = GetComponent<RetroshipController>();
        _barrier = GetComponent<BarrierController>();
        HealthSlider.maxValue = _retroship.MaxHealth;
        BarrierSlider.maxValue = _barrier.MaxHealth;
    }

    void Update ()
    {
        HealthSlider.value = _retroship.Health;
        BarrierSlider.value = _barrier.Health;
    }
}
