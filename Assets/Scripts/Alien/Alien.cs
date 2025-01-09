using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Alien : MonoBehaviour
{
    [Header("Spawn Alien")]
    protected HealthSystem healthSystem;
    [SerializeField] float spawnVelocity = 10f;
    [SerializeField] MilkyHeal heal;
    [SerializeField] int healRarity = 2;
    [SerializeField] public GameObject boomEffect;
    protected NavMeshAgent agent;
    public static int Score = 0;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.deathEvent.AddListener(Die);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }

    protected void Die()
    {
        PoolManager.instance.KillAlien(this);
        int r = Random.Range(0, healRarity);
        if(r==0) Instantiate(heal,transform.position + Vector3.up, transform.rotation);
        Score += 1;
        HUD.singleton.Score.text = "Score : " + Score;
    }

    public abstract IEnumerator Behave();

    public IEnumerator SpawnCoroutine(Vector3 pos)
    {
        RaycastHit hit;
        transform.position = pos;

        while (!Physics.Raycast(transform.position, -Vector3.up, out hit, 0.1f) && transform.position.y > 0f)
        {
            transform.position -= Vector3.up * spawnVelocity * Time.deltaTime;
            yield return 0;
        }
        agent.enabled = true;
        if(gameObject.activeSelf) StartCoroutine(Behave());
    }
    
}
