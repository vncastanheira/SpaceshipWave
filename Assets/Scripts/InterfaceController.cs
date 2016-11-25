using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RetroshipController))]
public class InterfaceController : MonoBehaviour
{
    RetroshipController _controller;

    public Slider HealthSlider;

    void Start ()
    {
        _controller = GetComponent<RetroshipController>();
        HealthSlider.maxValue = _controller.MaxHealth;
    }

    void Update ()
    {
        HealthSlider.value = _controller.Health;
    }
}
