// 프리팹을 이용하여 쿠키를 복사하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieGenerator : MonoBehaviour
{
    public GameObject cookiePrefab;

    // 쿠키를 얼마나 복사할건지
    int horizontal_CookieNum = 26;
    int vertical_CookieNum = 20;

    // 쿠키를 어느 간격만큼 복사할건지
    float horizontal_Interval = 0.2f;
    float vertical_Interval = 0.296f;

    // 기능: 반복문을 이용하여 쿠키를 복사한다
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

    // 기능: 실질적으로 쿠키를 복사하는 함수
    void copyCookie(float horizontalOffset, float verticalOffset)
    {
        Instantiate(cookiePrefab, new Vector3(transform.position.x + horizontalOffset, 
            transform.position.y + verticalOffset, transform.position.z), Quaternion.identity);
    }
}
