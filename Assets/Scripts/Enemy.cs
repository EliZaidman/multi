using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    private string bulletTag = "Bullet";

    [SerializeField] int HP;
    [SerializeField] int DMG;
    public PhotonView PhotonView { get; private set; }

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (HP <=0)
        {
            this.PhotonView.RPC("Dead", RpcTarget.All);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == bulletTag)
        {
            this.PhotonView.RPC("TookDMG", RpcTarget.All);
            Destroy(collision.gameObject);
        }
    }

    [PunRPC]
    private void TookDMG()
    {
        HP -= DMG;
    }

    [PunRPC]
    private void Dead()
    {
        BankManager.Instance._Silver += 100;
        Destroy(gameObject);
    }
}
