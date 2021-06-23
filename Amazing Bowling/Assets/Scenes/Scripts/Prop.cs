using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle; // 미리가지고 있지 않고 실시간으로
    public float hp = 10f;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp<=0)
        {
            ParticleSystem instance = Instantiate(explosionParticle, transform.position,transform.rotation);

            GameManager.instance.AddScore(score);
            
            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false); // Prop을 파괴하지 않는다. 모두 파괴후 재생성하면 렉이걸릴 수 있으므로 on/off
        }
    }
}
