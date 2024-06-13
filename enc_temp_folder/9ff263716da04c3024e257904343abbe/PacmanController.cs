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

    MovementController mvController; // MovementController ����

    // Start is called before the first frame update
    void Start()
    {
        directorController = GameObject.Find("����������Ʈ").GetComponent<DirectorController>();
        GameObject pacman = GameObject.Find("�Ѹ�");
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
        if (collision.tag == "��Ű")
        {
            Destroy(collision.gameObject);
            score += 10;
            Debug.Log("����" + score);
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
                // ���� ��ġ 2D ����Ʈ ����
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                // ���� ��ġ 2D ����Ʈ ����
                secondPressPos = new Vector2(t.position.x, t.position.y);

                // �� ����Ʈ�� ���� ����
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // 2D ���� ����ȭ
                currentSwipe.Normalize();

                // MovementController�� moveDirection�� �������� ���⿡ ���� ����
                if (mvController != null)
                {
                    // ���� ��������
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.up;
                        targetRotation = Quaternion.Euler(0, 0, -90);
                        Debug.Log("���� ��������");
                    }
                    // �Ʒ��� ��������
                    else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        mvController.moveDirection = Vector2.down;
                        targetRotation = Quaternion.Euler(0, 0, 90);
                        Debug.Log("�Ʒ��� ��������");
                    }
                    // �������� ��������
                    else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.left;
                        targetRotation = Quaternion.Euler(0, 0, 0);
                        Debug.Log("�������� ��������");
                    }
                    // ���������� ��������
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        mvController.moveDirection = Vector2.right;
                        targetRotation = Quaternion.Euler(0, 0, 180);
                        Debug.Log("���������� ��������");
                    }
                }
            }
        }
    }
    void RotateSprite()
    {
        // ��������Ʈ ȸ�� ����
        if (mvController != null)
        {
            transform.rotation = targetRotation;
        }
    }
}
