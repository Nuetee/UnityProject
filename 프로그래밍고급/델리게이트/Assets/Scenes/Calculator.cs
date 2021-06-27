using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    delegate float Calculate(float a, float b); // 앞으로 탄생될 delegate들의 원형, float을 반환하고 float 두개를 parameter로 받는 형태의 함수  만 가능

    Calculate onCalculate;

    void Start()
    {
        onCalculate = Sum; // Sum을 명부에 등록
        onCalculate += Sub;
        onCalculate -= Sub;
        onCalculate += Mul;
    }

    public float Sum(float a, float b)
    {
        Debug.Log(a + b);
        return a + b;
    }

    public float Sub(float a, float b)
    {
        Debug.Log(a - b);
        return a - b;
    }

    public float Mul(float a, float b)
    {
        Debug.Log(a * b);
        return a * b;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("결과값: " + onCalculate(1, 10));
        }
    }
}
