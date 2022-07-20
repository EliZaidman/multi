using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RealTimeUpdater : MonoBehaviour,IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GameManaging.Instance._Silver);
            stream.SendNext(GameManaging.Instance.HP);
        }
        else
        {
            GameManaging.Instance._Silver = (int)stream.ReceiveNext();
            GameManaging.Instance.HP = (int)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
