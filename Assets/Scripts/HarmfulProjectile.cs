
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HarmfulProjectile : MonoBehaviour
{
    [SerializeField] public int damage;
    public Vector3 direction;


    private void OnCollisionEnter(Collision collision)
    {
        HealthSystem health;
        if (collision.collider.TryGetComponent(out health))
        {
            health.addHealth(-1 * damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        HealthSystem health;
        if (collider.TryGetComponent(out health))
        {
            health.addHealth(-1 * damage);
        }
        Destroy(gameObject);
    }
}
