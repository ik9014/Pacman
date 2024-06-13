// 반복문을 이용해 복사한 쿠키를 게임맵에 맞게 조절하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieDeleter : MonoBehaviour
{

    // 기능: '쿠키' 태그를 가진 오브젝트에 충돌 시 해당 오브젝트 삭제
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "쿠키")
        {
            Destroy(collision.gameObject);
        }
    }
}
