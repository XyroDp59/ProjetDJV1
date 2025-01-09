using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : HarmfulProjectile
{
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
