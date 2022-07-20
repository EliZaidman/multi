using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.position = _playerSpawn.position;
    }
}
