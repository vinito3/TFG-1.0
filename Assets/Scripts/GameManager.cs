using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks {
    
    [Header("My Player Prefab")]
    public Player playerPrefab;

    [HideInInspector]
    public Player localPlayer;

    private void Awake() {
        if (!PhotonNetwork.IsConnected) {
            SceneManager.LoadScene("Main Menu");
            return;
        }
    }

    private void Start() {
        Player.RefreshPlayerInstance(ref localPlayer, playerPrefab);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) {
        base.OnPlayerEnteredRoom(newPlayer);
        Player.RefreshPlayerInstance(ref localPlayer, playerPrefab);
        Debug.Log(newPlayer.NickName + " joined the game!");
    }

}
