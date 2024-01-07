using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }

    void LogCallback(string logString, string stackTrace, LogType type)
    {
        logText.text = logString;
        //Or Append the log to the old one
        //logText.text += logString + "\r\n";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
