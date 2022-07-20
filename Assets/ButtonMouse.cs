using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMouse : MonoBehaviour
{
    [SerializeField] Image myImage;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManaging.Instance.PhotonView.RPC("StartGame", RpcTarget.AllBufferedViaServer);

                if (myImage != null)
                    myImage.color = Color.green;
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
