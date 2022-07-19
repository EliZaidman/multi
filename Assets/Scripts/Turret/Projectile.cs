using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const string _enemyTag = "Enemy";
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == _enemyTag)
        {
            // enemy health --
        }

        Destroy(gameObject);
    }
}
