using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ChatHandler : MonoBehaviour {

    public int maxMessages = 10;

    public GameObject chatPanel, textObject;

    public TMP_InputField chatInputField;

    [SerializeField]
    private List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (chatInputField.isFocused) {
            if (chatInputField.text.Length > 0) {
                if (Input.GetKeyDown(KeyCode.Return)) {
                    SendMessageToChat(chatInputField.text);
                    chatInputField.text = "";                
                }
            }    
        }

    }

    public void SendMessageToChat(string text) {

        if (messageList.Count >= maxMessages) {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);     
        }

        Message message = new Message();
        message.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        message.textObject = newText.GetComponent<TextMeshProUGUI>();
        message.textObject.text = message.text;

        messageList.Add(message);

    }
    
}

[System.Serializable]
public class Message {

    public string text;
    public TextMeshProUGUI textObject;
    
}