using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthBar;
    private int currentHealth;

    public UnityEvent deathEvent;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void addHealth(int health)
    {
        currentHealth = Mathf.Clamp( currentHealth + health, 0, maxHealth);
        if(!healthBar.gameObject.activeSelf) { healthBar.gameObject.SetActive(true); }
        healthBar.UpdateFill(((float)currentHealth) / ((float)maxHealth));
        if (currentHealth <= 0 )
        {
            deathEvent.Invoke();
            currentHealth = maxHealth;
        }
    }

}
