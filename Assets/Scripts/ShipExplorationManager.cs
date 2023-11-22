using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.Encryption;
using TMPro;
using UnityEngine.SceneManagement;

public class ShipExplorationManager : MonoBehaviourPunCallbacks
{
    public GameObject[] explorationShipPrefabs;
    public Transform[] startingPositions;
    //public GameObject[] finisherTextUi;

    private bool didQuit = false; 

    //public TextMeshProUGUI timeText;

    //public List<GameObject> lapTriggers = new List<GameObject>();

    public static ShipExplorationManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;

            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Constants.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            {
                Debug.Log((int)playerSelectionNumber);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                Vector3 instantiatePosition = startingPositions[actorNumber - 1].position;
                PhotonNetwork.Instantiate(explorationShipPrefabs[(int)playerSelectionNumber].name, instantiatePosition, startingPositions[actorNumber - 1].rotation);
            }
        }

        /*foreach (GameObject go in finisherTextUi)
        {
            go.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!didQuit && Input.GetKey(KeyCode.Escape))
        {
            QuitButton();
            didQuit = true;
            Invoke(nameof(didQuit), 2f);
        }
    }

    public void ResetDidQuit()
    {
        didQuit = false;
    }

    public void QuitButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
