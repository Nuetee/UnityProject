using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Worker : MonoBehaviour
{
    // delegate void Work(); => 이런 형태의 delegate를 많이 쓰므로 Unity내에서 제공

    Action work; // Action == delegate void Action()

    void MoveBricks()
    {
        Debug.Log("벽돌을 옮겼다");
    }

    void DigIn()
    {
        Debug.Log("땅을 팠다");
    }

    void Start()
    {
        work += MoveBricks;
        work += DigIn;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            work();
        }        
    }
}
