using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winUI;

    public ItemBox[] itemBoxes;

    public bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //키보드가 눌리는 순간, 그냥 GetKey는 눌려있는 동안
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //SceneManager.LoadScene("Main");
            //이름 대신 buildindex사용 가능. File -> Build Setting에 먼저 등록
            SceneManager.LoadScene(0);
        }

        if(isGameOver == true)
        {
            return;
        }

        int count = 0;
        for(int i = 0; i < 3; i++)
        {
            if(itemBoxes[i].isOveraped == true)
            {
                count++;
            }
        }

        if(count>=3)
        {
            Debug.Log("게임 승리");
            isGameOver = true;
            winUI.SetActive(true);
        }
    }
}
