using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp; //layer구분을 통해 prop식별, layer는 여러 레이어를 동시에 선택하여 필터링 할 수 있음.
    public ParticleSystem explosionParticle;
    public AudioSource explosionAudio;

    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float lifeTime = 10f;
    public float explosionRadius = 20f;

    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void OnDestroy(){
        GameManager.instance.OnBallDestroy();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Physics는 물리관련 함수 집합
        //OverlapSphere는 가상의 구를 그려 그 구 안에 있는 모든 collider를 배열로 가져온다.
        //parameter는 옵션
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius,whatIsProp);

        for(int i = 0; i<colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            //폭발의 위치와 폭발력을 통해 내가 얼마나 튕겨나가야 되는지 계산해주는 편의기능을 제공하는 함수
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);
        }
        //충돌이 일어나는 순간 폭발 effect의 부모를 없애준다.(Ball로부터 벗어나도록)
        explosionParticle.transform.parent = null;

        explosionParticle.Play();
        explosionAudio.Play();

        //GameManager.instance.OnBallDestroy();

        Destroy(explosionParticle.gameObject, explosionParticle.duration); // .duration은 해당 object의 running time, running time이후에 destroy
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition) // 내 위치로부터 target까지의 위치를 계산해서 damage계산
    {
        Vector3 explosionToTarget = targetPosition- transform.position;

        float distance = explosionToTarget.magnitude; // magnitude는 벡터의 길이

        float edgeToCenterDistance = explosionRadius - distance;
        float percentage = edgeToCenterDistance/explosionRadius;

        float damage = maxDamage * percentage;

        damage = Mathf.Max(damage, 0); // Collider가 가상의 구에 걸쳐있을 때, Collider의 중심이 구 바깥에 있는 경우 damage가 음수가 되어 체력 회복, 이같은 경우 방지

        return damage;
    }
}
