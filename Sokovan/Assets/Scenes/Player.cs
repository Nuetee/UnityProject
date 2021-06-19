using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

    public float speed = 10f;
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameOver == true)
        {
            return;
        }
        // A <-         -> D
        // -1.0    0    +1.0

        //수평방향에 대해서 키보드나 조이스틱 입력을 했을 때, -1~+1사이로 값을 줌
        //숫자로 주는 이유는 조이스틱의 살짝 밀고 당기고를 구현
        float inputX = Input.GetAxis("Horizontal");

        float inputZ = Input.GetAxis("Vertical");

        /*
        RigidBody에 힘을 주는것
        playerRigidbody.AddForce(inputX * speed, 0, inputZ * speed);
        */
        Vector3 velocity = new Vector3(inputX, 0, inputZ);

        velocity *= speed;

        float fallSpeed = playerRigidbody.velocity.y;
        velocity.y = fallSpeed;
        //즉시 속도를 주기
        playerRigidbody.velocity = velocity;

        /*유저입력, GetKey는 키보드 입력
        if(Input.GetKey(KeyCode.W))
        {
            playerRigidbody.AddForce(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidbody.AddForce(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigidbody.AddForce(0, 0, -speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidbody.AddForce(speed, 0, 0);
        }
        */
    }
}