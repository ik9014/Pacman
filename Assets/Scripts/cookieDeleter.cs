// �ݺ����� �̿��� ������ ��Ű�� ���Ӹʿ� �°� �����ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieDeleter : MonoBehaviour
{

    // ���: '��Ű' �±׸� ���� ������Ʈ�� �浹 �� �ش� ������Ʈ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "��Ű")
        {
            Destroy(collision.gameObject);
        }
    }
}
