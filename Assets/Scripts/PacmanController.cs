// �Ѹ��� ��� ������ �������� �����ϴ� ��ũ��Ʈ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanController : MonoBehaviour
{
    public int score = 0; // ����
    public int lives = 3; // HP

    public AudioSource death_sound; // �״� �Ҹ�

    // ��ġ �Է��� �����ϴµ� ����� ����
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    Quaternion targetRotation; // �Ѹ� ��������Ʈ ���� ���� (��� �ٶ󺸰� ������ ����)
    private float rayDistance = 0.2f; // ����ĳ��Ʈ�� �Ÿ�

    public SpriteRenderer spriteRenderer;
    public DirectorController directorController;

    MovementController mvController;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        directorController = GameObject.Find("����������Ʈ").GetComponent<DirectorController>();
        GameObject pacman = GameObject.Find("�Ѹ�");
      
        mvController = pacman.GetComponent<MovementController>();
    }

    void Update()
    {
        Swipe(); // �޴��� ��ġ �ν�
        RotateSprite(); // �Ѹ� ��������Ʈ ���� ����

        // ������ �����Կ� ���� �Ѹ��� �ӵ��� ���ݾ� �ø���
        if (score >= 800)
        {
            mvController.moveSpeed = 1.15f;
        }
        if (score >= 1600)
        {
            mvController.moveSpeed = 1.3f;
        }

        // ���� Ŭ����� �ӵ��� 0���� �����Ѵ�
        if (score == 2540)
        {
            mvController.moveSpeed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ű�� ������ ��Ű�� �����ϰ� ������ 10�� �ø���
        if (collision.tag == "��Ű")
        {
            Destroy(collision.gameObject);
            score += 10;

            directorController.sound_eatCookie(this);
        }

        // ���ɿ� ����� �� ����
        if (collision.tag == "����")
        {
            HandleDeath(); // HP ����
        }
    }

    // ���: HP�� �����ϴ� �Լ���
    void HandleDeath()
    {
        lives--; // HP 1 ����
        death_sound.Play(); // �״� �Ҹ� ���

        // HP �� �� ���
        if (lives <= 0)
        {
            gameObject.SetActive(false);
        }
        // ���� HP�� ����������
        else
        {
            Revive();
        }
    }

    // ���: �Ѹ��� ��Ȱ��Ų��
    void Revive()
    {
        // ���� �������� �ʱ�ȭ�Ѵ�
        mvController.rb.velocity = mvController.moveDirection * mvController.moveSpeed;
        mvController.moveDirection = Vector2.left;
        targetRotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(0, -0.75f, 0);
    }

    // ���: �޴��� ��ġ(��������)�� �ν��Ѵ�
    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                // ���� ��ġ ���� ����
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                // ���� ��ġ ���� ����
                secondPressPos = new Vector2(t.position.x, t.position.y);

                // �� ����Ʈ�� ���� ����
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // 2D ���� ����ȭ
                currentSwipe.Normalize();

                // ������ ������ �������� ���⿡ ���� ����
                if (mvController != null)
                {
                    // ���� ��������
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.up;
                        targetRotation = Quaternion.Euler(0, 0, -90);
                    }

                    // �Ʒ��� ��������
                    else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.down;
                        targetRotation = Quaternion.Euler(0, 0, 90);
                    }

                    // �������� ��������
                    else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.left;
                        targetRotation = Quaternion.Euler(0, 0, 0);
                    }

                    // ���������� ��������
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.right;
                        targetRotation = Quaternion.Euler(0, 0, 180);
                    }
                }
            }
        }
    }
    
    // ���: �Ѹ� ȸ�� ����
    void RotateSprite()
    {
        if (mvController != null)
        {
            transform.rotation = targetRotation;
        }
    }
}
