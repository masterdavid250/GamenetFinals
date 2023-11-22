using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class ShipLapController : MonoBehaviourPunCallbacks
{
    private List<GameObject> LapTriggers = new List<GameObject>();

    public enum RaiseEventsCode
    {
        WhoFinishedEventCode = 0 
    }

    private int finishOrder = 0; 

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject lapTriggers in ShipRacingManager.instance.lapTriggers)
        {
            LapTriggers.Add(lapTriggers); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent; 
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventsCode.WhoFinishedEventCode)
        {
            object[] data = (object[])photonEvent.CustomData; 

            string nickNameOfFinishedPlayer = (string)data[0];
            finishOrder = (int)data[1];
            int viewID = (int)data[2];  
            Debug.Log(nickNameOfFinishedPlayer + " " + finishOrder);

            GameObject orderUITextGameObject = ShipRacingManager.instance.finisherTextUi[finishOrder - 1];
            orderUITextGameObject.SetActive(true);

            if (viewID == photonView.ViewID)
            {
                orderUITextGameObject.GetComponent<TextMeshProUGUI>().text = finishOrder + " " + nickNameOfFinishedPlayer + "[YOU]";
                orderUITextGameObject.GetComponent<TextMeshProUGUI>().color = Color.red; 
            }
            else
            {
                orderUITextGameObject.GetComponent<TextMeshProUGUI>().text = finishOrder + " " + nickNameOfFinishedPlayer; 
            }

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LapTriggers.Contains(other.gameObject))
        {
            int indexOfTrigger = LapTriggers.IndexOf(other.gameObject);
            LapTriggers[indexOfTrigger].SetActive(false);
            if (other.name == "EndTrigger")
            {
                GameFinished(); 
            }
        }
    }

    private void GameFinished()
    {
        Debug.Log("GAME END");

        finishOrder += 1; 
        string nickName = photonView.Owner.NickName;
        int viewID = photonView.ViewID; 

        object[] data = new object[] { nickName, finishOrder, viewID };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions{ Receivers = ReceiverGroup.All, CachingOption = EventCaching.AddToRoomCache };
        SendOptions sendOptions = new SendOptions { Reliability = false }; 
        PhotonNetwork.RaiseEvent((byte)RaiseEventsCode.WhoFinishedEventCode, data, raiseEventOptions, sendOptions); 
        //Sp
    }
}
