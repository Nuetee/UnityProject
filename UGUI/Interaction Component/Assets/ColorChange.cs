using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorChange : MonoBehaviour
{
    public void ChangeColor(int index)
    {
        Color color = Color.white;
        
        switch(index)
        {
            case 0:
                color = Color.red;
                break;
            
            case 1:
                color = Color.blue;
                break;

            case 2:
                color = Color.green;
                break;

            case 3:
                color = Color.white;
                break;
        }
        
        GetComponent<Renderer>().material.color = color;
    }
}
