using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State {
        Idle, Ready, Tracking
    }

    private State state { // property : 사용할 때는 변수처럼 사용하지만 안에서는 함수처럼 동작, 함수 대신 사용하는 이유는 변수처럼 사용하여 처리를 간결하게 보이게함. 함수 대체 가능
        // 바깥에서 state에 '='로 값을 전달할 때 set안의 처리들이 실행 됨. 바깥에서 주는 값들은 'value'라는 키워드로 받아옴
        set{
            switch(value)
            {
                case State.Idle:
                targetZoomSize = roundReadyZoomSize;
                break;
                
                case State.Ready:
                targetZoomSize = readyShotZoomSize;
                break;

                case State.Tracking:
                targetZoomSize = trackingZoomSize;
                break;
            }
        }
    }

    private Transform target;

    //카메라가 target에 바로 가서 붙는것이 아니라 smooth하게 움직여 붙도록 하는 기능추가
    private float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;

    private Camera cam; // 카메라를 가져와서 zoom in/ out을 위해 사용
    private float targetZoomSize = 5f;
    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f; //포탄을 발사할 준비
    private const float trackingZoomSize = 10;
    private float lastZoomSpeed; // 마지막 순간에 값이 얼마나 변경됐는지 알아야 Unity가 smooth기능 적용 가능
    
    void Awake()
    {
        // GetComponent는 나에게 붙어있는 Component, GetComponentInChildren는 자식의 Component를 뒤져서 가져옴
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;

        //SmoothDamp(현재위치, 가고싶은위치, 마지막순간의 변화량, 지연시간) => 매순간에 값을 부드럽게 꺾어서 지정해줌.
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref lastMovingVelocity, smoothTime);

        transform.position = smoothPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate() // 그냥 Update는 렉으로 프레임이 떨어지면 그만큼만 실행, 그러나 FixedUpdate는 항상 같은 간격으로 실행
    {
        if(target != null)
        {
            Zoom();
            Move();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
