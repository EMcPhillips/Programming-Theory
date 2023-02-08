using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float cooldown = 2.0f;

    private void Start()
    {
        StartCoroutine(DestroyProjectileCooldown());
    }

    IEnumerator DestroyProjectileCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        Destroy(gameObject);
    }

}
