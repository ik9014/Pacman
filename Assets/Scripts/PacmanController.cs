// 팩맨이 어떻게 방향을 결정할지 관리하는 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{
    public int score = 0; // 점수
    public int lives = 3; // HP

    public AudioSource death_sound; // 죽는 소리

    // 터치 입력을 감지하는데 사용할 변수
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Quaternion targetRotation; // 팩맨 스프라이트 각도 조정 (어디를 바라보고 있을지 결정)
    private float rayDistance = 0.2f; // 레이캐스트의 거리

    public SpriteRenderer spriteRenderer;
    public DirectorController directorController;

    MovementController mvController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        directorController = GameObject.Find("감독오브젝트").GetComponent<DirectorController>();
        GameObject pacman = GameObject.Find("팩맨");
      
        mvController = pacman.GetComponent<MovementController>();
    }

    void Update()
    {
        Swipe(); // 휴대폰 터치 인식
        RotateSprite(); // 팩맨 스프라이트 각도 조정

        // 점수가 증가함에 따라 팩맨의 속도를 조금씩 올린다
        if (score >= 800)
        {
            mvController.moveSpeed = 1.15f;
        }
        if (score >= 1600)
        {
            mvController.moveSpeed = 1.3f;
        }

        // 게임 클리어시 속도를 0으로 설정한다
        if (score == 2540)
        {
            mvController.moveSpeed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 쿠키를 먹으면 쿠키를 삭제하고 점수를 10점 올린다
        if (collision.tag == "쿠키")
        {
            Destroy(collision.gameObject);
            score += 10;

            directorController.sound_eatCookie(this);
        }

        // 유령에 닿았을 시 실행
        if (collision.tag == "유령")
        {
            HandleDeath(); // HP 감소
        }
    }

    // 기능: HP가 감소하는 함수다
    void HandleDeath()
    {
        lives--; // HP 1 감소
        death_sound.Play(); // 죽는 소리 재생

        // HP 가 다 닳면
        if (lives <= 0)
        {
            gameObject.SetActive(false);
        }
        // 아직 HP가 남아있으면
        else
        {
            Revive();
        }
    }

    // 기능: 팩맨을 부활시킨다
    void Revive()
    {
        // 각종 설정들을 초기화한다
        mvController.rb.velocity = mvController.moveDirection * mvController.moveSpeed;
        mvController.moveDirection = Vector2.left;
        targetRotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(0, -0.75f, 0);
    }

    // 기능: 휴대폰 터치(스와이프)를 인식한다
    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                // 시작 터치 지점 저장
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                // 종료 터치 지점 저장
                secondPressPos = new Vector2(t.position.x, t.position.y);

                // 두 포인트로 벡터 생성
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // 2D 벡터 정규화
                currentSwipe.Normalize();

                // 움직일 방향을 스와이프 방향에 따라 설정
                if (mvController != null)
                {
                    // 위로 스와이프
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.up;
                        targetRotation = Quaternion.Euler(0, 0, -90);
                    }

                    // 아래로 스와이프
                    else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.down;
                        targetRotation = Quaternion.Euler(0, 0, 90);
                    }

                    // 왼쪽으로 스와이프
                    else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.left;
                        targetRotation = Quaternion.Euler(0, 0, 0);
                    }

                    // 오른쪽으로 스와이프
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.right;
                        targetRotation = Quaternion.Euler(0, 0, 180);
                    }
                }
            }
        }
    }
    
    // 기능: 팩맨 회전 적용
    void RotateSprite()
    {
        if (mvController != null)
        {
            transform.rotation = targetRotation;
        }
    }
}
