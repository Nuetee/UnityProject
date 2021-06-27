using UnityEngine;

public class LateUpdateFollow : MonoBehaviour
{
    public Transform targetToFollow;

    private void LateUpdate() // Update 함수와 같은 주기로 실행되지만 매번 Update함수가 종료되는 시점에 실행
    {
        transform.position = targetToFollow.position;
        transform.rotation = targetToFollow.rotation;
    }
}