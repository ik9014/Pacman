// �Ѹǰ� ���ɵ��� �������� �����ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 1f;
    public Vector2 moveDirection; // �̵� ����
    public LayerMask tileLayer;
    public float rayDistance = 0.2f; // ����ĳ��Ʈ �Ÿ� ����
    
    void Start()
    {
        tileLayer = LayerMask.GetMask("Walllayer");
        rb = GetComponent<Rigidbody2D>();

        // ���� ���� ��, �̵������� �������� ����
        moveDirection = Vector2.left;
    }

    void Update()
    {
        teleport(); // �߾���� �̿��

        // �ӵ��� 0�� �ƴ� �� (��, ���� �浹���� ���� �����϶�) ����
        if (moveDirection != Vector2.zero)
        {
            CheckCollisionAndMove();
        }
        
    }

    // ���: ȭ�� ������ ����� �ݴ������� Ƣ����� �Ѵ�
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

    // ���: ������ �浹 ����
    void CheckCollisionAndMove()
    {
        // ����ĳ��Ʈ�� �߻��Ͽ� ������ �浹�� ����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        // �浹�ߴٸ�
        if (hit.collider != null)
        {  
            rb.velocity = Vector2.zero; // �ӵ��� 0���� ����
        }

        // �浹���� �ʾҴٸ�
        else
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }
}
