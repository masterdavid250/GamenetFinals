using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI; 
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Camera shipCamera;
    public TextMeshProUGUI playerNameText; 

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("rc"))
        {
            GetComponent<ShipMovement>().enabled = photonView.IsMine;
            GetComponent<ShipMovement>().isControlEnabled = photonView.IsMine;
            //GetComponentInChildren<ShipMovement>().enabled = photonView.IsMine;
            //GetComponentInChildren<ShipMovement>().isControlEnabled = photonView.IsMine;
            shipCamera.enabled = photonView.IsMine;
        }
        else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("dr"))
        {
            GetComponent<ShipMovement>().enabled = photonView.IsMine;
            GetComponent<ShipMovement>().isControlEnabled = photonView.IsMine;
            GetComponent<ShipLapController>().enabled = photonView.IsMine;
            //GetComponentInChildren<ShipMovement>().enabled = photonView.IsMine;
            //GetComponent<LapController>().enabled = photonView.IsMine;
            //GetComponentInChildren<ShipMovement>().isControlEnabled = photonView.IsMine;
            shipCamera.enabled = photonView.IsMine;
        }

        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }

        //SetRacerUI(); 
    }
}
