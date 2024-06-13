using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DirectorController : MonoBehaviour
{
    public PacmanController pacmancontroller;
    // ���� �ؽ�Ʈ��
    public Text scoreText; // ����
    public Text livesText; // HP
    public Text gameOverText; // ���ӿ���
    public Text gameClear; // ����Ŭ����

    // ���� �Ҹ���
    public AudioSource munch_1; // ��Ű ������ 1
    public AudioSource munch_2; // ��Ű ������ 2
    public AudioSource Siren; // ���� ���� ���

    int currentMunch = 0; // ��Ű ������ �Ҹ��� ���ѹݺ��ϴµ� ���δ�
    
    // ���� �ÿ��� ���ӿ���, ����Ŭ���� �ؽ�Ʈ�� �����.
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        gameClear.gameObject.SetActive(false);
    }

    
    void Update()
    {
        scoreText.text = pacmancontroller.score.ToString(); // ���� ǥ��
        livesText.text = "HP " + pacmancontroller.lives.ToString(); // HP ǥ��

        // Ŭ���� �ÿ��� (��, ��Ű�� �� ������) ����Ŭ���� �ؽ�Ʈ�� �����ش�.
        if (pacmancontroller.score >= 2540)
        {
            gameClear.gameObject.SetActive(true);
            Destroy(Siren);
        }

        // HP�� �� ��� ���� ���� �ؽ�Ʈ�� �����ش�.
        if (pacmancontroller.lives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            Destroy(Siren);

            // ȭ�� ��ġ �� 'StartScene'���� �����ȯ. ��, ������� �ϴ� ���̴�.
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("StartScene");
            }
        }  
    }

    // ���: ��Ű�� ���� �� �Ҹ��� ���� �Լ�
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
