using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using TMPro;

public class ChatterManager : ChatterManagerBehavior {

    public Transform chatContent;
    public GameObject chatMessage;

    public void WriteMessgae(TMP_InputField sender) {
        if (!string.IsNullOrEmpty(sender.text) && sender.text.Trim().Length > 0) {
            sender.text = sender.text.Replace("\r", "").Replace("\n", "");
            networkObject.SendRpc(RPC_TRANSMIT_MESSGAE, Receivers.All, sender.text.Trim(), PhotonNetwork.NickName);
            sender.text = "";
            sender.ActivateInputField();
        }
    }

    public override void TransmitMessgae(RpcArgs args) {

        string message = args.GetNext<string>();
        string username = args.GetNext<string>();

        if (string.IsNullOrEmpty(message))
            return;

        GameObject newMessage = Instantiate(chatMessage, chatContent);
        TextMeshProUGUI content = newMessage.GetComponent<TextMeshProUGUI>();

        content.text = string.Format(content.text, username, message);

    }

}
