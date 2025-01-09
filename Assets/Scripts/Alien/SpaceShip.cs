using System.Collections;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Wave Parameters")]
    public static int Wave = 0;
    [SerializeField] public HealthSystem health;
    private bool isAlive;
    [SerializeField] float betweenWaveDelay = 5f;
    WaitForSeconds respawnDelay;

    [SerializeField] private float spawnCooldown = 2f;
    [SerializeField] private float spawnAlienCooldown = 2f;
    WaitForSeconds spawnAlienDelay;
    [SerializeField] private float spawnAccelerationCoeff = 0.99f;

    [Header("Moving Parameters")]
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;
    [SerializeField] private float speed = 10;
    [SerializeField] private float movingAccelerationCoeff = 1.1f;

    Vector3 initialPos;
    Vector3 target;
    Vector3 dir;

    private void Start()
    {
        health.deathEvent.AddListener(Die);
        respawnDelay = new WaitForSeconds(betweenWaveDelay);
        initialPos = transform.position;
        Die();
        spawnAlienDelay = new WaitForSeconds(spawnAlienCooldown);
    }


    private void Behave()
    {
        transform.position = initialPos;
        isAlive = true;
        transform.GetChild(0).gameObject.SetActive(true);
        //TODO : play spawn Spaceship animation

        target = transform.position;
        StartCoroutine(GoToTarget());


        IEnumerator GoToTarget()
        {
            while (isAlive)
            {
                // S'il est a destination, spawn un alien
                if (Vector3.Magnitude(transform.position - target) < 1)
                {
                    target = new Vector3(
                        Random.Range(minPos.position.x, maxPos.position.x),
                        transform.position.y,
                        Random.Range(minPos.position.z, maxPos.position.z)
                    );
                    dir = (target - transform.position).normalized;
                    PoolManager.instance.SpawnAlien( transform.position );
                    yield return spawnAlienDelay;
                }
                // Sinon, se déplace vers un point aléatoire
                transform.position += dir * speed * Time.deltaTime;
                yield return 0;
            }
            yield return null;
        }

    }

    private void Die()
    {
        Wave += 1;
        HUD.singleton.Wave.text = "Wave : " + Wave;
        isAlive = false;
        //TODO : play death animation
        transform.GetChild(0).gameObject.SetActive(false);
        spawnCooldown *= spawnAccelerationCoeff;
        speed *= movingAccelerationCoeff;
        StartCoroutine(myCoroutine());
        PoolManager.instance.KillAllAliens();

        IEnumerator myCoroutine()
        {
            Debug.Log("start waiting");
            yield return respawnDelay;
            Debug.Log("start behaving");
            Behave();
        }
    }

}
