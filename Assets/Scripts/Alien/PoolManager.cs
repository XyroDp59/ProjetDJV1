using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoolManager : MonoBehaviour
{
    private List<Alien> activeAliens = new List<Alien>();
    private List<Alien> killedAliens = new List<Alien>();

    [SerializeField] private List<Alien> aliensPrefab = new List<Alien>();

    [SerializeField] private int ALIEN_NUMBER;

    public static PoolManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SpawnAlien();
    }

    private Alien SpawnAlien()
    {
        if(killedAliens.Count <= 0)
        {
            for (int i = 0; i < ALIEN_NUMBER; i++)
            {
                int r = Random.Range(0, aliensPrefab.Count);
                Alien newAlien = Instantiate(aliensPrefab[r], transform);
                killedAliens.Add(newAlien);
            }
        }
        Alien alien = killedAliens[0];
        alien.gameObject.SetActive(true);
        
        killedAliens.Remove(alien); activeAliens.Add(alien);
        return alien;
    }
    
    public void SpawnAlien(Vector3 pos)
    {
        Alien alien = SpawnAlien();
        StartCoroutine(alien.SpawnCoroutine(pos));
    }

    public void KillAlien(Alien alien)
    {
        if (!activeAliens.Contains(alien)) throw new InvalidOperationException("The alien was not active but you tried to kill it !");

        killedAliens.Add(alien); activeAliens.Remove(alien);
        alien.gameObject.SetActive(false);

        StartCoroutine(BoomCoroutine());
        IEnumerator BoomCoroutine(){
            alien.boomEffect.transform.parent = alien.transform.parent;
            alien.boomEffect.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            alien.boomEffect.SetActive(false);
            alien.boomEffect.transform.parent = alien.transform;
        }
    }

    public void KillAllAliens()
    {
        while(activeAliens.Count > 0)
        {
            KillAlien(activeAliens[0]);
        }
    }

}
