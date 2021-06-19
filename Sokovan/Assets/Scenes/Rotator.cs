using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    /*
    transform component는 모든 object들이 가지고 있으므로 이렇게 일일이 선언해주지 않아도 된다.
    public Transform myTransform;
    */

    // Update is called once per frame
    void Update()
    {
        // transform을 통해 transform shortcut제공. 내 transform을 받는다.
        transform.Rotate(60 * Time.deltaTime, 60 * Time.deltaTime, 60 * Time.deltaTime);
        // Time.deltaTime은 화면이 한번 깜박이는 시간(한프레임의 시간)
        // 초당 60프레임이면 Time.deltaTime은 1/60
    }
}
