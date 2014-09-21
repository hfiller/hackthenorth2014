using UnityEngine;
using System.Collections;

public class RandomMatchmaker : MonoBehaviour {
	// Use this for initialization
	private bool isHost = false;
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
		isHost = true;
    }

    void OnJoinedRoom()
    {
		Debug.Log (isHost);
		Vector3 location = new Vector3();
		location.Set (-0.0f, -0.0f, -1.00f);
		GameObject player = PhotonNetwork.Instantiate("New Prefab", location, Quaternion.identity, 0);
        player.GetComponent<myThirdPersonController>().isControllable = true;
		player.GetComponent<myThirdPersonController>().isHost = isHost;
		isHost = false;
    }

	// Update is called once per frame
	void Update () 
    {

	}
}
