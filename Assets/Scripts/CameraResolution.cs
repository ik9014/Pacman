// ȭ�� ������ �������ִ� ��ũ��Ʈ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // ���� ī�޶��� ����Ʈ ������ �������� �ڵ�
        Rect viewportRect = cam.rect;

        // ���ϴ� ���� ���� ������ ����ϴ� �ڵ�
        float screenAspectRatio = (float)Screen.width / Screen.height;
        float targetAspectRatio = 9f / 16f; // ���ϴ� ���� ���� ����

        // ȭ�� ���� ���� ������ ���� ����Ʈ ������ ����
        if (screenAspectRatio < targetAspectRatio)
        {
            // ȭ���� �� '����'�� (���ΰ� �� ��ٸ�) ���θ� ����
            viewportRect.height = screenAspectRatio / targetAspectRatio;
            viewportRect.y = (1f - viewportRect.height) / 2f;
        }
        else
        {
            // ȭ���� �� '�д�'�� (���ΰ� �� ��ٸ�) ���θ� ����
            viewportRect.width = targetAspectRatio / screenAspectRatio;
            viewportRect.x = (1f - viewportRect.width) / 2f;
        }

        // ������ ����Ʈ ������ ī�޶� ����
        cam.rect = viewportRect;
    }
}