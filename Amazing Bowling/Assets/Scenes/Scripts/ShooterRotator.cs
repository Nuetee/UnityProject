using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
  private enum RotateState {
      Idle, Vertical, Horizontal, Ready // 실제로는 숫자이지만 문자로 보이도록
  }

  private RotateState state= RotateState.Idle;
  public float verticalRotateSpeed = 360f;
  public float horizontalRotateSpeed = 360f;
  public BallShooter ballShooter;
  void Update()
  {
      switch(state)
      {
          case RotateState.Idle:
            if(Input.GetButtonDown("Fire1"))
            {
                state = RotateState.Horizontal;
            }
          break;

          case RotateState.Horizontal:
            if(Input.GetButton("Fire1"))
            {
                transform.Rotate(new Vector3(0,horizontalRotateSpeed * Time.deltaTime,0));
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                state = RotateState.Vertical;
            }
          break;

          case RotateState.Vertical:
            if(Input.GetButton("Fire1"))
            {
                transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                state = RotateState.Ready;
                ballShooter.enabled = true; // ball barrel을 다 돌리고 난 후 ball shooter를 켜준다.
            }
          break;
           
          case RotateState.Ready:
          break;
      }
  }

  private void OnEnable()
  {
    transform.rotation = Quaternion.identity; //identity는 0,0,0 회전한 상태
    state = RotateState.Idle;
    ballShooter.enabled = false;
  }
}
