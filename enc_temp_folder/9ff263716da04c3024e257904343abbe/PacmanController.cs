using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{
    public int score = 0;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Quaternion targetRotation;
    private float rayDistance = 0.2f;

    public DirectorController directorController;

    MovementController mvController; // MovementController 참조

    // Start is called before the first frame update
    void Start()
    {
        directorController = GameObject.Find("감독오브젝트").GetComponent<DirectorController>();
        GameObject pacman = GameObject.Find("팩맨");
        if (pacman != null)
        {
            mvController = pacman.GetComponent<MovementController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        RotateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "쿠키")
        {
            Destroy(collision.gameObject);
            score += 10;
            Debug.Log("점수" + score);
            directorController.sound_eatCookie(this);
        }
    }

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                // 시작 터치 2D 포인트 저장
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                // 종료 터치 2D 포인트 저장
                secondPressPos = new Vector2(t.position.x, t.position.y);

                // 두 포인트로 벡터 생성
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // 2D 벡터 정규화
                currentSwipe.Normalize();

                // MovementController의 moveDirection을 스와이프 방향에 따라 설정
                if (mvController != null)
                {
                    // 위로 스와이프
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.up;
                        targetRotation = Quaternion.Euler(0, 0, -90);
                        Debug.Log("위로 스와이프");
                    }
                    // 아래로 스와이프
                    else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.down;
                        targetRotation = Quaternion.Euler(0, 0, 90);
                        Debug.Log("아래로 스와이프");
                    }
                    // 왼쪽으로 스와이프
                    else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.left;
                        targetRotation = Quaternion.Euler(0, 0, 0);
                        Debug.Log("왼쪽으로 스와이프");
                    }
                    // 오른쪽으로 스와이프
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.right;
                        targetRotation = Quaternion.Euler(0, 0, 180);
                        Debug.Log("오른쪽으로 스와이프");
                    }
                }
            }
        }
    }
    void RotateSprite()
    {
        // 스프라이트 회전 적용
        if (mvController != null)
        {
            transform.rotation = targetRotation;
        }
    }
}
