using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AlienShooter : Alien
{
    [SerializeField] HarmfulProjectile bullet;
    [SerializeField] private float cooldown = 2;
    [SerializeField] private int maxDistance = 30;
    [SerializeField] private GameObject shootEffect;

    bool isActivated = false;

    public override IEnumerator Behave()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            transform.LookAt(Player.playerHitBox);
            if (IsPlayerVisible())
            {
                agent.isStopped = true;
                yield return new WaitForSeconds(cooldown);
                Shoot(bullet);
            }
            else { 
                agent.isStopped = false;
                Vector3 agentTarget = Player.playerHitBox.position 
                    - 0.5f * maxDistance *(Player.playerHitBox.position - transform.position).normalized ;
                agent.SetDestination(agentTarget);
            }
            yield return null;
        }
    }

    private bool IsPlayerVisible()
    {
        RaycastHit hit;
        Vector3 start = bullet.transform.position;
        Vector3 dir = Player.playerHitBox.position - start;

        Ray ray = new Ray(start, dir);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(start, hit.point, UnityEngine.Color.magenta, 0.0f, true);
            bool b = (Vector3.Distance(hit.point, Player.playerHitBox.position) <= 5f);
            return b;
        }
        else
        {
            return false;
        }
    }

    private void Shoot(HarmfulProjectile bullet)
    {
        Debug.Log("SHOOT");
        Vector3 bulletPos = bullet.transform.position;

        HarmfulProjectile b = Instantiate(bullet, bulletPos, bullet.transform.rotation);

        Vector3 dir = Player.playerHitBox.position - transform.position;

        b.direction = dir.normalized;
        b.gameObject.SetActive(true);
    }
}