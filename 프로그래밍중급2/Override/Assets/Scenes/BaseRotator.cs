using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotator : MonoBehaviour
{
    public float speed = 60f;

    void Update()
    {
        Rotate();
    }

    protected virtual void Rotate() // virtual : 자식들이 Override할 수 있게 만듦
    {
        transform.Rotate(speed * Time.deltaTime,0,0);
    }
}
