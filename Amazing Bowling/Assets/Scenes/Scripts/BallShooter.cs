using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow cam;
    public Rigidbody ball;
    public Transform firepos;
    public Slider powerSlider;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public AudioClip chargingClip;
    public float minForce = 15f;
    public float maxForce = 30f;
    public float chargingTime = 0.75f;

    private float currentForce;
    private float chargeSpeed;
    private bool fired;

    private void OnEnable() // Component가 꺼져있다가 켜지는 순간 매번 발동이 된다.(여기선 BallShooter가 켜질때)
    {
        currentForce = minForce;
        powerSlider.value = minForce;
        fired = false;
    }

    private void Start()
    {
        chargeSpeed = (maxForce - minForce)/chargingTime;
    }

    private void Update()
    {
        if(fired == true)
        {
            return;
        }
        
        powerSlider.value = minForce;

        //현재 힘이 maxforce보다 크고 아직 발사되지 않았을때
        if(currentForce>=maxForce && !fired)
        {
            currentForce = maxForce;
            // 발사처리
            Fire();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            currentForce = minForce;
            
            shootingAudio.clip = chargingClip; // 오디오 클립 교체
            shootingAudio.Play();
        }
        else if(Input.GetButton("Fire1") && !fired)
        {
            currentForce = currentForce + chargeSpeed * Time.deltaTime;

            powerSlider.value = currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !fired)
        {
            // 발사 처리
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody ballInstance = Instantiate(ball, firepos.position, firepos.rotation);

        ballInstance.velocity = currentForce * firepos.forward; // .forward 는 transform의 앞쪽(z) 방향을 Vector3로 반환

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentForce = minForce;

        cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
