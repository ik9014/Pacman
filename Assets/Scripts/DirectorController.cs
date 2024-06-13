using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectorController : MonoBehaviour
{
    public PacmanController pacmancontroller;
    // 각종 텍스트들
    public Text scoreText; // 점수
    public Text livesText; // HP
    public Text gameOverText; // 게임오버
    public Text gameClear; // 게임클리어

    // 각종 소리들
    public AudioSource munch_1; // 쿠키 먹을시 1
    public AudioSource munch_2; // 쿠키 먹을시 2
    public AudioSource Siren; // 게임 진행 브금

    int currentMunch = 0; // 쿠키 먹을시 소리를 무한반복하는데 쓰인다
    
    // 시작 시에는 게임오버, 게임클리어 텍스트를 숨긴다.
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        gameClear.gameObject.SetActive(false);
    }

    
    void Update()
    {
        scoreText.text = pacmancontroller.score.ToString(); // 점수 표시
        livesText.text = "HP " + pacmancontroller.lives.ToString(); // HP 표시

        // 클리어 시에는 (즉, 쿠키를 다 먹으면) 게임클리어 텍스트를 보여준다.
        if (pacmancontroller.score >= 2540)
        {
            gameClear.gameObject.SetActive(true);
            Destroy(Siren);
        }

        // HP가 다 닳면 게임 오버 텍스트를 보여준다.
        if (pacmancontroller.lives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            Destroy(Siren);

            // 화면 터치 시 'StartScene'으로 장면전환. 즉, 재시작을 하는 것이다.
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("StartScene");
            }
        }  
    }

    // 기능: 쿠키를 먹을 시 소리를 내는 함수
    public void sound_eatCookie(PacmanController pacmanController)
    {
        if (currentMunch == 0)
        {
            munch_1.Play();
            currentMunch = 1;
        }
        else if (currentMunch == 1)
        {
            munch_2.Play();
            currentMunch = 0;
        }
    }
}
