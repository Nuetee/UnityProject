using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintInputField : MonoBehaviour
{
    public void OnInputFieldTextChanged(string newText)
    {
        Debug.Log("타이핑 하는중!");
        Debug.Log(newText);
    }

    public void OnInputFieldTextDone(string newText)
    {
        Debug.Log("타이핑 끝!");
        Debug.Log(newText);
    }
}
