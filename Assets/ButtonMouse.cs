using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMouse : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManaging.Instance.PhotonView.RPC("StartGame", RpcTarget.AllBufferedViaServer);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Enemy>().PhotonView.RPC("KillYourself", RpcTarget.AllBufferedViaServer);
            GameManaging.Instance.PhotonView.RPC("TakeDMG", RpcTarget.AllBufferedViaServer);
        }
    }
}
