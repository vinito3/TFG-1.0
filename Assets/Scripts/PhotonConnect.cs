using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon;
using TMPro;

public class PhotonConnect : MonoBehaviourPunCallbacks {

    public TMP_InputField tfCreateRoomName;
    public TMP_InputField tfJoinRoomName;

    public GameObject menuPanel;
    public GameObject startPanel;
    public GameObject roomObject;

    public Transform content;

    public bool triesToConnectToMaster;
    public bool triesToConnectToRoom;

    private void Awake() {

        triesToConnectToMaster = false;
        triesToConnectToRoom = false;

    }

    private void Update() {
        DontDestroyOnLoad(gameObject);
        if(startPanel != null)
            startPanel.SetActive(!PhotonNetwork.IsConnected && !triesToConnectToMaster);
        if(menuPanel != null)
            menuPanel.SetActive(PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom);
    }

    public void OnClickConnectToMaster() {

        PhotonNetwork.OfflineMode = false;
        //PhotonNetwork.NickName = "PlayerName";
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.1";

        triesToConnectToMaster = true;
        // PhotonNetwork.ConnectToMaster("localhost", 9090, "88067633 - 768e-48a3 - 908a - be3942ef81ed"); // manual connection
        PhotonNetwork.ConnectUsingSettings(); // conexion automatica 
            
    }

    public void OnClickConnectToRoom() {

        if (!PhotonNetwork.IsConnected)
            return;

        triesToConnectToRoom = true;
        // PhotonNetwork.JoinRoom("Room Name"); //Error: OnJoinRoomFailed()
        PhotonNetwork.JoinRandomRoom(); //Error: OnJoinRandomRoomFailed()

    }

    public void OnClickCreateRoom() {
        if (tfCreateRoomName.text.Length <= 0)
            return;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;

        PhotonNetwork.CreateRoom(tfCreateRoomName.text, roomOptions); //Error: OnCreateRoomFailed()
    }

    public void OnClickJoinRoom() {
        if (tfJoinRoomName.text.Length > 0)
            PhotonNetwork.JoinRoom(tfJoinRoomName.text); //Error: OnJoinRoomFailed()
    }
    
    public override void OnJoinedRoom() {
        base.OnJoinedRoom();
        triesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        SceneManager.LoadScene("Game");
    }

    public override void OnJoinRandomFailed(System.Int16 returnCode, System.String message) {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnCreateRoomFailed(System.Int16 returnCode, System.String message) {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Tu puta madre");
        triesToConnectToRoom = false;
    }

    public override void OnDisconnected(DisconnectCause cause) {
        base.OnDisconnected(cause);
        triesToConnectToMaster = false;
        triesToConnectToRoom = false;
        // Debug.Log(cause);
    }
    
    public override void OnConnectedToMaster() {
        base.OnConnectedToMaster();
        triesToConnectToMaster = false;
        //Debug.Log("Connected to Master!");
        PhotonNetwork.JoinLobby();
        //Debug.Log("Joined lobby!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        
        ClearRoomListView();

        DisplayCachedRooms(roomList);

    }

    private void DisplayCachedRooms(List<RoomInfo> rooms) {

        foreach (RoomInfo room in rooms) {

            GameObject item = Instantiate(roomObject, content);

            item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/" + room.MaxPlayers;
            
        }

    }

    private void ClearRoomListView() {

        for (int i = 0; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }
        
    }
    
}
