// 유령들의 방향을 결정하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private MovementController mvController;
    private PacmanController pacmanController;

    void Start()
    {
        mvController = GetComponent<MovementController>();
    
        GameObject pacman = GameObject.Find("팩맨");
        pacmanController = pacman.GetComponent<PacmanController>();
    }
   
    void Update()
    {
        if (mvController.rb.velocity == Vector2.zero)
        {
            Collide(); // 벽에 충돌시 이동방향 결정
        }

        score_move(); // 점수 증가 -> 속도 업

        // HP가 0이면 속도를 0으로 설정
        if (pacmanController.lives == 0)
        {
            mvController.rb.velocity = Vector2.zero;
        }
        // 게임 클리어시 (즉, 쿠키를 다 먹으면) 속도를 0으로 설정
        if (pacmanController.score == 2540)
        {
            mvController.rb.velocity = Vector2.zero;
        }

    }

    // 기능: 점수가 증가함에 따라 유령들의 속도를 증가시킨다
    void score_move()
    {
        if (pacmanController.score >= 500)
        {
            mvController.moveSpeed = 1.2f;
        }
        if (pacmanController.score >= 1000)
        {
            mvController.moveSpeed = 1.4f;
        }
        if (pacmanController.score >= 1500)
        {
            mvController.moveSpeed = 1.6f;
        }
        if (pacmanController.score >= 2000)
        {
            mvController.moveSpeed = 1.8f;
        }
    }

    // 기능: 유령들의 움직임을 관장하는 함수모음집
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("노드"))
        {
            CenterNode();
        }
        else if (collision.CompareTag("좌우노드"))
        {
            LRNode();
        }
        else if (collision.CompareTag("좌우_상노드"))
        {
            LRNode_Plus ();
        }
        else if (collision.CompareTag("모서리_상좌노드"))
        {
            C_ULNode();
        }
        else if (collision.CompareTag("모서리_상우노드"))
        {
            C_URNode();
        }
        else if (collision.CompareTag("모서리_하좌노드"))
        {
            C_DLNode();
        }
        else if (collision.CompareTag("모서리_하우노드"))
        {
            C_DRNode();
        }
    }

    // 기능: 유령이 모서리에 부딪혔을 때 1
    void C_ULNode()
    {
        if (mvController.moveDirection == Vector2.left)
        {
            mvController.moveDirection = Vector2.down;
        }else if (mvController.moveDirection == Vector2.up)
        {
            mvController.moveDirection = Vector2.right;
        }
    }

    // 기능: 유령이 모서리에 부딪혔을 때 2
    void C_URNode()
    {
        if (mvController.moveDirection == Vector2.right)
        {
            mvController.moveDirection = Vector2.down;
        }
        else if (mvController.moveDirection == Vector2.up)
        {
            mvController.moveDirection = Vector2.left;
        }
    }

    // 기능: 유령이 모서리에 부딪혔을 때 3
    void C_DLNode()
    {
        if (mvController.moveDirection == Vector2.left)
        {
            mvController.moveDirection = Vector2.up;
        }
        else if (mvController.moveDirection == Vector2.down)
        {
            mvController.moveDirection = Vector2.right;
        }
    }

    // 기능: 유령이 모서리에 부딪혔을 때 4
    void C_DRNode()
    {
        if (mvController.moveDirection == Vector2.right)
        {
            mvController.moveDirection = Vector2.up;
        }
        else if (mvController.moveDirection == Vector2.down)
        {
            mvController.moveDirection = Vector2.left;
        }
    }

    // 기능: 골목길에서 유령 방향전환 1
    void LRNode()
    {
        int N_random = Random.Range(1, 3);

        if (mvController.moveDirection == Vector2.right)
        {
            switch (N_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.right;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.up;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.left)
        {
            switch (N_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.left;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.up;
                    break;
            }
        }
    }

    // 기능: 골목길에서 유령 방향전환 2
    void LRNode_Plus()
    {
        int N_random = Random.Range(1, 3);

        if (mvController.moveDirection == Vector2.right)
        {
            switch (N_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.right;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.down;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.left)
        {
            switch (N_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.left;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.down;
                    break;
            }
        }
    }

    // 기능: 유령이 스폰지점에서 나오게 한다
    void CenterNode()
    {
        if (mvController.moveDirection == Vector2.down)
        {
            int N_random = Random.Range(1, 3);
            switch (N_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.left;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.right;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.left || mvController.moveDirection == Vector2.right)
        {
            mvController.moveDirection = Vector2.up;
        }
    }

    // 기능: 유령이 평평한 벽에 부딪혔을 때 방향 결정
    void Collide()
    {
        int C_random = Random.Range(1, 3);

        if (mvController.moveDirection == Vector2.up)
        {
            switch (C_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.right;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.left;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.down)
        {
            switch (C_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.right;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.left;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.right)
        {
            switch (C_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.up;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.down;
                    break;
            }
        }
        else if (mvController.moveDirection == Vector2.left)
        {
            switch (C_random)
            {
                case 1:
                    mvController.moveDirection = Vector2.up;
                    break;
                case 2:
                    mvController.moveDirection = Vector2.down;
                    break;
            }
        }
        mvController.rb.velocity = mvController.moveDirection * mvController.moveSpeed;
    }
}
