// 팩맨과 유령들의 움직임을 관장하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    public Vector2 moveDirection; // 이동 방향
    public LayerMask tileLayer;
    public float rayDistance = 0.2f; // 레이캐스트 거리 조정
    
    void Start()
    {
        tileLayer = LayerMask.GetMask("Walllayer");
        rb = GetComponent<Rigidbody2D>();

        // 게임 시작 시, 이동방향을 왼쪽으로 설정
        moveDirection = Vector2.left;
    }

    void Update()
    {
        teleport(); // 중앙통로 이용시

        // 속도가 0이 아닐 때 (즉, 벽에 충돌하지 않은 상태일때) 실행
        if (moveDirection != Vector2.zero)
        {
            CheckCollisionAndMove();
        }
        
    }

    // 기능: 화면 밖으로 벗어나면 반대편으로 튀어나오게 한다
    void teleport()
    {
        if (this.transform.position.x > 2.8f)
        {
            this.transform.position = new Vector3(-2.8f, this.transform.position.y, 0);
        }
        if (this.transform.position.x < -2.8f)
        {
            this.transform.position = new Vector3(2.8f, this.transform.position.y, 0);
        }
    }

    // 기능: 벽과의 충돌 감지
    void CheckCollisionAndMove()
    {
        // 레이캐스트를 발사하여 벽과의 충돌을 감지
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        // 충돌했다면
        if (hit.collider != null)
        {  
            rb.velocity = Vector2.zero; // 속도를 0으로 설정
        }

        // 충돌하지 않았다면
        else
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }
}
