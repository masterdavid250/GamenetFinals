using Photon.Pun;
using Photon.Realtime; 
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipCountdownManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    public float timeToStartRace = 2.0f;

    private void Start()
    {
        timerText = ShipRacingManager.instance.timeText;
        this.GetComponent<ShipMovement>().isControlEnabled = false;
        this.GetComponent<ShipShooting>().isControlEnabled = false;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timeToStartRace > 0)
            {
                timeToStartRace -= Time.deltaTime;
                photonView.RPC("SetTime", RpcTarget.AllBuffered, timeToStartRace);
            }
            else if (Time.deltaTime > 0)
            {
                photonView.RPC("StartRace", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    public void SetTime(float time)
    {
        if (timerText)
        {
            if (time > 0)
            {
                timerText.text = time.ToString("F1");
            }
            else
            {
                timerText.text = "";
            }
        }
    }

    [PunRPC]
    public void StartRace()
    {
        GetComponent<ShipMovement>().isControlEnabled = true;
        this.GetComponent<ShipShooting>().isControlEnabled = true;
        this.enabled = false;
    }
}
