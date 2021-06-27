using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteraction : MonoBehaviour
{
    public LayerMask whatIsTarget;
    private Camera playerCam;
    public float distance = 100f; // 100미터 앞쪽으로 광선을 날린다.
    private Transform moveTarget;
    private float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = Camera.main; // 현재 활성화 된 메인카메라를 가져와서 넣어준다.
    }

    // Update is called once per frame
    void Update()
    {
        // 광선이 날아갈 원점 지정
        // 카메라 상에 위치를 찍어주면 그 위치가 실제 게임 세상에서 어떤 위치인자 알려주는 함수 ViewportToWorldPoint
        // 화면상의 위치(new Vector3(0.5f, 0.5f, 0))를 찍어주면 그 화면 상의 점이 World position으로 어디인지 알려줌
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); // Vector3(0.5f, 0.5f, 0)는 카메라로 보고있는 정중앙 지점이다.

        //광선을 쏠 때 어느 방향으로 쏠지 지정
        Vector3 rayDir = playerCam.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDir);

        // 광선을 눈으로 확인, 개발자용 함수, 게임 내에서는 안보임
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit; // RaycastHit은 Raycast에 의한 정보를 담아다 주는 단순 정보 컨테이너
            // Ray caster는 물리처리이기 때문에 Physics 함수 집합에 들어있다.
            // 광선에 어떤 collider가 걸리면 if문 만족
            // (광선 시작위치, 광선 시작방향, 사정거리, 레이어), 레이어는 없으면 모든 오브젝트에 대해서
            // 광선 시작위치, 광선 시작방향 대신 바로 Ray형 class 변수를 넣어줘도 됨
            if(Physics.Raycast(ray, out hit, distance, whatIsTarget)) //out 키워드는 입력으로 들어간 값(hit)이 내부에서 어떤 값이 생겨서 빠져나온다는 의미
            {
                GameObject hitTarget = hit.collider.gameObject;

                hitTarget.GetComponent<Renderer>().material.color = Color.red;
                moveTarget = hitTarget.transform;
                targetDistance = hit.distance;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(moveTarget != null)
            {
                moveTarget.GetComponent<Renderer>().material.color = Color.white;
            }
            moveTarget = null;
        }

        if(moveTarget != null)
        {
            moveTarget.position = ray.origin + ray.direction * targetDistance;
        }
    }
}
