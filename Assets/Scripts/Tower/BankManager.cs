using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Photon.Pun;
using TMPro;

public class BankManager : MonoBehaviour
{

    public int _Silver = 1000;

    private int HP;
    [SerializeField] int DMG;
    [SerializeField] int maxHP;

    [SerializeField] private int _TurretCost;
    public static BankManager Instance { get; private set; }

    public PhotonView PhotonView { get; private set; }

    public TextMeshProUGUI moneyGUI;
    public TextMeshProUGUI hpGUI;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        HP = maxHP;
        PhotonView = GetComponent<PhotonView>();
        moneyGUI.text = _Silver.ToString();
        hpGUI.text = HP.ToString();
    }


    [PunRPC]
    private int CalculateCorrentMoney()
    {
        return _Silver;
    }

    [PunRPC]
    private void BuyTurret()
    {
        if (_Silver >= 100)
        {
            _Silver -= _TurretCost;
            moneyGUI.text =_Silver.ToString();
        }
        else
        {
            print("Not Enough Money");
        }
    }

    [PunRPC]
    private void TakeDMG()
    {
        HP -= DMG;
        hpGUI.text = HP.ToString();
    }
}
