// �������� �̿��Ͽ� ��Ű�� �����ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieGenerator : MonoBehaviour
{
    public GameObject cookiePrefab;

    // ��Ű�� �󸶳� �����Ұ���
    int horizontal_CookieNum = 26;
    int vertical_CookieNum = 20;

    // ��Ű�� ��� ���ݸ�ŭ �����Ұ���
    float horizontal_Interval = 0.2f;
    float vertical_Interval = 0.296f;

    // ���: �ݺ����� �̿��Ͽ� ��Ű�� �����Ѵ�
    void Start()
    {
        for (int i = 0; i < vertical_CookieNum; i++)
        {
            float horizontal_CurrentInterval = 0;
            float vertical_CurrentInterval = vertical_Interval * i;

            for (int j = 0; j < horizontal_CookieNum; j++)
            {
                copyCookie(horizontal_CurrentInterval, vertical_CurrentInterval);
                horizontal_CurrentInterval += horizontal_Interval;
            }
        }
    }

    // ���: ���������� ��Ű�� �����ϴ� �Լ�
    void copyCookie(float horizontalOffset, float verticalOffset)
    {
        Instantiate(cookiePrefab, new Vector3(transform.position.x + horizontalOffset, 
            transform.position.y + verticalOffset, transform.position.z), Quaternion.identity);
    }
}
