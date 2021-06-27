using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // for Scene 재시작

public class GameManager : MonoBehaviour
{
    public void OnPlayerDead()
    {
        Invoke("Restart", 5f); // 5초의 지연시간 이후 Restart 발동
    }
    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
