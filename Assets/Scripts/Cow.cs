using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Cow : MonoBehaviour
{
    public static List<Cow> Cows = new List<Cow>();
    public static int Friends = 0;
    [SerializeField] private Animator animator;
    private NavMeshAgent agent;
    private Vector3 spawn;

    void Start()
    {
        Cows.Add(this);
        Friends++;
        UpdateCount();
        spawn = transform.position;

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        StartCoroutine(Behave());
        IEnumerator Behave()
        {
            while(true)
            {
                float r = Random.Range(0.0f, 1.0f);
                yield return new WaitForSeconds(1f + r);
                Vector3 dist = new Vector3(Random.Range(0.0f, 1.0f), 0, Random.Range(0.0f, 1.0f));
                if(agent.isActiveAndEnabled) agent.SetDestination(spawn + dist);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(DeathCoroutine());

        IEnumerator DeathCoroutine(){
            animator.SetTrigger("death");
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            Friends--;
            UpdateCount();
            if (Friends == 0)
            {
                HUD.singleton.GameOver("THE IINVADERS TOOK EVERY COWS");
            }
        }
    }
    private void UpdateCount()
    {
        HUD.singleton.Friends.text = "Friends : " + Friends;
    }
}