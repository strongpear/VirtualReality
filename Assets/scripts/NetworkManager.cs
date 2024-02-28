using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectedToServer();
    }

    private void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Attempting connection to server");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected");
        base.OnConnectedToMaster();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player Joined Room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public int GetPlayerCount()
    {
        if (PhotonNetwork.InRoom)
        {
            Room currentRoom = PhotonNetwork.CurrentRoom;
            int playerCount = currentRoom.PlayerCount;
            Debug.Log("Number of Players: " + playerCount);
            return playerCount;
        }
        else
        {
            Debug.Log("Not in Room");
            return 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
