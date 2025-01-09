using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject gunEffect;
    [SerializeField] GameObject boomEffect;
    [SerializeField] GameObject spaceShipMesh;
    [SerializeField] GameObject bullet;

    public void Launch()
    {
        gunEffect.SetActive(true);
        bullet.SetActive(true);
        StartCoroutine(Behave());
    }
    IEnumerator Behave()
    {
        while (true)
        {
            bullet.transform.position += (spaceShipMesh.transform.position - bullet.transform.position).normalized * 20f * Time.deltaTime;
            yield return null;
            if (Vector3.Distance(spaceShipMesh.transform.position, bullet.transform.position) < 0.5f)
            {
                boomEffect.SetActive(true);
                bullet.SetActive(false);
                spaceShipMesh.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene(1);
            }
        }
    }
}
