using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float Force;
    public float Lifetime;
    Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        rigidBody.AddForce(direction * Force);

        Destroy(gameObject, Lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
