using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends messages for components
/// </summary>
public class CollisionMessenger : MonoBehaviour
{
    public string[] TriggerEnter;
    public string[] CollisionEnter;
    public bool AlsoUpwards;
    public SendMessageOptions MessageOptions;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var msg in CollisionEnter)
        {
            SendMessage(msg, collision, MessageOptions);
            if (AlsoUpwards)
                SendMessageUpwards(msg, MessageOptions);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var msg in TriggerEnter)
        {
            SendMessage(msg, other, MessageOptions);
            if (AlsoUpwards)
                SendMessageUpwards(msg, other, MessageOptions);
        }
    }
}
