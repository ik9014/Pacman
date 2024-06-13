// ���ɵ��� ������ �����ϴ� ��ũ��Ʈ

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
    
        GameObject pacman = GameObject.Find("�Ѹ�");
        pacmanController = pacman.GetComponent<PacmanController>();
    }
   
    void Update()
    {
        if (mvController.rb.velocity == Vector2.zero)
        {
            Collide(); // ���� �浹�� �̵����� ����
        }

        score_move(); // ���� ���� -> �ӵ� ��

        // HP�� 0�̸� �ӵ��� 0���� ����
        if (pacmanController.lives == 0)
        {
            mvController.rb.velocity = Vector2.zero;
        }
        // ���� Ŭ����� (��, ��Ű�� �� ������) �ӵ��� 0���� ����
        if (pacmanController.score == 2540)
        {
            mvController.rb.velocity = Vector2.zero;
        }

    }

    // ���: ������ �����Կ� ���� ���ɵ��� �ӵ��� ������Ų��
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

    // ���: ���ɵ��� �������� �����ϴ� �Լ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("���"))
        {
            CenterNode();
        }
        else if (collision.CompareTag("�¿���"))
        {
            LRNode();
        }
        else if (collision.CompareTag("�¿�_����"))
        {
            LRNode_Plus ();
        }
        else if (collision.CompareTag("�𼭸�_���³��"))
        {
            C_ULNode();
        }
        else if (collision.CompareTag("�𼭸�_�����"))
        {
            C_URNode();
        }
        else if (collision.CompareTag("�𼭸�_���³��"))
        {
            C_DLNode();
        }
        else if (collision.CompareTag("�𼭸�_�Ͽ���"))
        {
            C_DRNode();
        }
    }

    // ���: ������ �𼭸��� �ε����� �� 1
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

    // ���: ������ �𼭸��� �ε����� �� 2
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

    // ���: ������ �𼭸��� �ε����� �� 3
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

    // ���: ������ �𼭸��� �ε����� �� 4
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

    // ���: ���濡�� ���� ������ȯ 1
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

    // ���: ���濡�� ���� ������ȯ 2
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

    // ���: ������ ������������ ������ �Ѵ�
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

    // ���: ������ ������ ���� �ε����� �� ���� ����
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
