using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZRotator : BaseRotator
{
    protected override void Rotate()
    {
        // base.Rotate(); => 부모 Rotate함수 내용을 쓰고 그 아래 덧붙일 수 있다.
        transform.Rotate(0,0,speed*Time.deltaTime);
    }
}
