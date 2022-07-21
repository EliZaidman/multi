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
    private AI  ai;
    public PhotonView PhotonView { get; private set; }

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        ai = gameObject.GetComponent<AI>();
    }

    private void Update()
    {
        if (HP <=0)
        {
            this.PhotonView.RPC("Dead", RpcTarget.AllBufferedViaServer);
        }

    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == bulletTag)
        {
            this.PhotonView.RPC("TookDMG", RpcTarget.All);
            Destroy(collision.gameObject);
            print("TookDmg");
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
        GameManaging.Instance._Silver += 10;
        GameManaging.Instance.moneyGUI.text = GameManaging.Instance._Silver.ToString();
        GameManaging.Instance.PhotonView.RPC("ScoreRPC", RpcTarget.AllBufferedViaServer);
        Destroy(gameObject);
    }

    [PunRPC]
    private void KillYourself()
    {
        Destroy(gameObject);
        SpawnManager.Instance.currentEnemies--;
    }
}
