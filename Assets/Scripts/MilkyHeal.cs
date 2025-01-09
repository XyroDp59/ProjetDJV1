using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkyHeal : MonoBehaviour
{
    [SerializeField] public int heal = 1;


    private void OnTriggerEnter(Collider collider)
    {
        HealthSystem health;
        if (collider.TryGetComponent(out health))
        {
            health.addHealth(heal);
            Destroy(gameObject);
        }
    }
}
