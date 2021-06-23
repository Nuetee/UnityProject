using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;
    public static GameManager instance;

    public GameObject readyPannel; // 게임이 라운드 대기상태이거나 끝난상태일 때 띄워줄 패널
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;
    public bool isRoundActive = false;
    private int score = 0;
    public ShooterRotator shooterRotator;
    public CamFollow cam;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    void Start()
    {
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if(GetBestScore() < score)
        {
            //"BestScore"를 key값으로 하고 score를 value로 하는 파일을 저장한다. 껐다 켜도 불러올 수 있다.
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore"); //"BestScore"를 key깂으로 하는 value가 저장되어있지 않으면 0이 return. 에러가 나지 않음
        return bestScore;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + GetBestScore();
    }

    public void OnBallDestroy() // Ball이 바닥에 닿아서 폭발할 때
    {
        UpdateUI();
        isRoundActive = false; // 라운드가 끝났다는 사실 명시, play상태를 탈출하는데 사용할 조건
    }

    public void Reset() // 라운드가 넘어갈 때마다 갈무리 역할
    {
        score = 0;
        UpdateUI();

        // 라운드를 다시 처음부터 시작
        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
        // READY
        onReset.Invoke();
        
        readyPannel.SetActive(true); // readyPannel을 켜준다
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle); // 카메라에게 타겟 지정
        shooterRotator.enabled = false; // 대기상태이므로 shooterRotator를 꺼준다. (조작 안되도록)

        isRoundActive = false;

        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

        // PLAY => 영원히 Ball이 바닥에 닿아 폭발할 때까지는 꺼지면 안된다. => 무한루프

        isRoundActive = true;
        readyPannel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready); //줌 인

        while(isRoundActive == true)
        {
            yield return null; // 한 프레임의 공백 시간
        }

        // END

        readyPannel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait For Next Round...";

        yield return new WaitForSeconds(3f);

        Reset();
    }
}
