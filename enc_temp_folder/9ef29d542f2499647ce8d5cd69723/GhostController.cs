using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyController : MonoBehaviour
{
    private MovementController mvController;
    private GameObject inky;
    private GameObject pacman;

    // Start is called before the first frame update
    void Start()
    {
        mvController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "노드")
        {
            CenterNode();
        }
    }

    void Raycast()
    {
        if (mvController.rb.velocity == Vector2.zero)
        {
            Collide();
        }
    }

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
        if (mvController.moveDirection == Vector2.left || mvController.moveDirection == Vector2.right)
        {
            mvController.moveDirection = Vector2.up;
        }
    }

    void Collide()
    {
        while (mvController.rb.velocity == Vector2.zero)
        {
            int C_random = Random.Range(1, 3); // 1~3 범위에서 난수 생성

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
                break;
            }

            if (mvController.moveDirection == Vector2.down)
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
                break;
            }

            if (mvController.moveDirection == Vector2.right)
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
                break;
            }

            if (mvController.moveDirection == Vector2.left)
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
                break;
            }
        }

        mvController.rb.velocity = mvController.moveDirection * mvController.moveSpeed;
    }
}
