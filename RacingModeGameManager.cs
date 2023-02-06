using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RacingModeGameManager : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public Transform[] InstantiatePositions;

    public Text TimeUIText;
    public GameObject[] FinishOrderUIGameObjects;

    public List<GameObject> lapTriggers = new List<GameObject>();

    //Singeleton Implementation
    public static RacingModeGameManager instance = null;

    private void Awake()
    {
        //check if instance already exists
        if (instance==null)
        {
            instance = this;
        }

        //If intance already exists and it is not !this!
        else if (instance != this)
        {
            //Then, destroy this. This enforces our singletton pattern, meaning rhat there can only ever be one instance of a GameManager
            Destroy(gameObject);

        }

        //To not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }


    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerRacingGame.PLAYER_SELECTION_NUMBER, out playerSelectionNumber ))
            {
                Debug.Log((int)playerSelectionNumber);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                Vector3 instantiatePosition = InstantiatePositions[actorNumber-1].position;

                PhotonNetwork.Instantiate(PlayerPrefabs[(int)playerSelectionNumber].name,instantiatePosition,Quaternion.identity);

            }


        }


        foreach (GameObject gm in FinishOrderUIGameObjects)
        {
            gm.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
