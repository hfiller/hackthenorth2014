using UnityEngine;
using System.Collections;

public class RandomMatchmaker : MonoBehaviour {
	// Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
	
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        GameObject monster = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
        monster.GetComponent<myThirdPersonController>().isControllable = true;
    }

	// Update is called once per frame
	void Update () 
    {

	}
}
