using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;


public class GameManaging : MonoBehaviour
{

    public int _Silver = 1000;

    public int HP;
    [SerializeField] int DMG;
    [SerializeField] int maxHP;

    [SerializeField] private int _TurretCost;
    public static GameManaging Instance { get; private set; }

    public PhotonView PhotonView { get; private set; }

    public TextMeshProUGUI moneyGUI;
    public TextMeshProUGUI hpGUI;
    public GameObject panel;


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

    }
    private void Update()
    {
        moneyGUI.text = _Silver.ToString();
        hpGUI.text = HP.ToString();
        if (HP <= 0)
        {
            this.PhotonView.RPC("LoosePanel", RpcTarget.AllBufferedViaServer);
        }
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
            moneyGUI.text = _Silver.ToString();
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

    [PunRPC]
    public void StartGame()
    {
        SpawnManager.Instance.StartGame++;
    }

    public void StartGameButton()
    {
        this.PhotonView.RPC("StartGame", RpcTarget.AllBufferedViaServer);
    }


    [PunRPC]
    public void LoosePanel()
    {
        panel.SetActive(true);
    }
}
