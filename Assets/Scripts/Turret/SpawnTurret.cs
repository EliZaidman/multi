using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnTurret : MonoBehaviour
{
    private PhotonView _photonView;
    [SerializeField] private GameObject _turret;
    private const string _playerTag = "Player";
    private bool _canSpawn = false;
    [SerializeField] private int cost;
    bool isActive = false;
    [SerializeField] private BoxCollider _turretSpawnerCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_turretSpawnerCollider.transform.position, new Vector3(1, 1, 1));
    }
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _turretSpawnerCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canSpawn && !isActive && GameManaging.Instance._Silver >= cost)
            {
                PhotonNetwork.Instantiate(_turret.name, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                GameManaging.Instance.PhotonView.RPC("BuyTurret", RpcTarget.AllBufferedViaServer);
                print("ME PUT TARTARTARTER");
                isActive = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _playerTag)
        {
            _canSpawn = true;
        }
        print("ME INSSIDE");


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == _playerTag)
        {
            _canSpawn = false;
        }
        print("ME EXIT");
    }

}
