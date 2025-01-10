using System.Collections;
using UnityEngine;

public class AlienKidnapper : Alien
{
    Cow kidnapTarget;
    float kidnapValue = -1f;
    [SerializeField] public float MaxKidnapValue = 1f;

    [SerializeField] private GameObject kidnapEffect;

    bool isActivated = false;

    public override IEnumerator Behave()
    {
        healthSystem.deathEvent.AddListener(kidnapperSafeDie);
        yield return new WaitForSeconds(1);

        while (gameObject.activeSelf)
        {
            if (kidnapValue >= MaxKidnapValue)
            {
                kidnapValue = -1f;
                kidnapEffect.SetActive(false);
                kidnapEffect.transform.parent = transform;
                if (kidnapTarget.gameObject.activeSelf) kidnapTarget.Die();
                yield return new WaitForSeconds(3);
            }
            else if (kidnapValue >= 0f)
            {
                kidnapValue += Time.deltaTime;
            }
            else
            {
                findNewTarget();
            }
            yield return null;
        }
    }

    private void findNewTarget()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Cow c in Cow.Cows)
        {
            Transform t = c.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && t.gameObject.activeSelf && t)
            {
                tMin = t;
                minDist = dist;
            }
        }
        agent.SetDestination(tMin.position);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.TryGetComponent(out kidnapTarget))
        {
            kidnapValue = 0.0f;
            kidnapEffect.transform.position = kidnapTarget.transform.position + 3* Vector3.up;
            kidnapEffect.transform.parent = kidnapTarget.transform;
            kidnapEffect.gameObject.SetActive(true);
            agent.SetDestination(kidnapTarget.transform.position);
            agent.isStopped = true;
            Debug.Log("found a cow");
        }
        if (!isActivated)
        {
            isActivated = true;
            StartCoroutine(Behave());
        }
    }

    public void kidnapperSafeDie()
    {
        kidnapEffect.transform.parent = transform;
    }
}