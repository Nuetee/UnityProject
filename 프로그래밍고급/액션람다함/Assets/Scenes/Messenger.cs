using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messenger : MonoBehaviour
{
    public delegate void Send(string reciever);

    Send onSend;

    void Start()
    {
        onSend += SendMail;
        onSend += SendMoney;
        onSend += man => Debug.Log("Assacinate " + man); // 람다 함수
        onSend += (string man) => { 
            Debug.Log("Assacinate " + man); 
            Debug.Log("Hide Body");
            };
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            onSend("Jaemin");
        }
    }

    void SendMail(string reciever)
    {
        Debug.Log("Mail sent to: " + reciever);
    }

    void SendMoney(string reciever)
    {
        Debug.Log("Money sent to: " + reciever);
    }
}
