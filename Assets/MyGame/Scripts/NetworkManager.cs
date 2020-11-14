using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public byte maxPlayers = 2;

    public GameObject canvasCharacterList;

    private void Start() {

        canvasCharacterList.gameObject.SetActive(false);

        PhotonNetwork.NickName = "Player" + Random.Range(100, 1000); // Player100-1000

        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnected() {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster() {
        Debug.Log("OnConnectedToMaster");

        PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("OnJoinRandomFailed");

        string roomName = "Room" + Random.Range(100, 1000); // Room101-999

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;

        PhotonNetwork.CreateRoom(roomName, roomOptions);

    }

    public override void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");

        Debug.Log("PlayerName: " + PhotonNetwork.LocalPlayer.NickName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log("OnPlayerEnteredRoom");

        if (!PhotonNetwork.IsMasterClient) {
            return;
        }

        CheckPlayerMax();
    }


    void CheckPlayerMax() {

        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers) {

            Hashtable props = new Hashtable {
                { "READY", true }
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        }

    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
        Debug.Log("OnRoomPropertiesUpdate");

        object ready_Obj;

        if (propertiesThatChanged.TryGetValue("READY", out ready_Obj)) {

            bool ready = (bool) ready_Obj;

            if (ready) {
                canvasCharacterList.gameObject.SetActive(true);
            }

        }

    }





}
