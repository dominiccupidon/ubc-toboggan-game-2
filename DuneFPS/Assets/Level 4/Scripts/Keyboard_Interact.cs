using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keyboard_Interact : MonoBehaviour
{
    // Start is called before the first frame update
    ViewScript viewScript;
    string key;
    void Start()
    {
        viewScript = GameObject.FindGameObjectWithTag("PassViewer").GetComponent<ViewScript>();
        key = transform.GetChild(0).GetComponent<TextMeshPro>().text;
    }

    private void OnMouseDown()
    {
        viewScript.keyPressed(key);
    }
}
