using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sends messages for components
/// </summary>
public class CollisionMessenger : MonoBehaviour
{
    public CollisionEnterEvent OnCollisionEnterEvent;
    public TriggerEnterEvent OnTriggerEnterEvent;

    //public string[] TriggerEnter;
    //public string[] CollisionEnter;
    //public bool AlsoUpwards;
    //public SendMessageOptions MessageOptions;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent.Invoke(collision);

        //foreach (var msg in CollisionEnter)
        //{
        //    SendMessage(msg, collision, MessageOptions);
        //    if (AlsoUpwards)
        //        SendMessageUpwards(msg, MessageOptions);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent.Invoke(other);

        //foreach (var msg in TriggerEnter)
        //{
        //    SendMessage(msg, other, MessageOptions);
        //    if (AlsoUpwards)
        //        SendMessageUpwards(msg, other, MessageOptions);
        //}
    }
}

[System.Serializable]
public class CollisionEnterEvent : UnityEvent<Collision> { }

[System.Serializable]
public class TriggerEnterEvent : UnityEvent<Collider> { }