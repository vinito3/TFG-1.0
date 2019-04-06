using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatInputfield : MonoBehaviour {

    public ChatterManager chatManager;

    private TMP_InputField inputfield;

    // Start is called before the first frame update
    void Start() {
        inputfield = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChanged() {
        if (inputfield.text.Contains("\n"))
            chatManager.WriteMessgae(inputfield);
    }

}
