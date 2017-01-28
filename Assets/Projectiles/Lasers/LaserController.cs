using UnityEngine;
using UnityEngine.Events;

public class LaserController : MonoBehaviour
{
    public float Force;
    public float Lifetime;
    Rigidbody rigidBody;
    Vector3 velocity;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Grid.instance.OnPause.AddListener(new UnityAction(Pause));
        Grid.instance.OnResume.AddListener(new UnityAction(Resume));
    }

    public void Launch(Vector3 direction)
    {
        rigidBody.AddForce(direction * Force);

        Destroy(gameObject, Lifetime);
    }

    void Pause()
    {
        velocity = rigidBody.velocity;
        rigidBody.Sleep();
    }

    void Resume()
    {
        rigidBody.WakeUp();
        rigidBody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }


}
