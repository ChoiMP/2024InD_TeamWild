using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadrotation : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // ���� ��ȭ �ӵ� (�ʴ� 1��)

    void Update()
    {
        // 0.01�� ������ ȸ�� ���� ���
        float rotationAngle = rotationSpeed * (0.01f / Time.deltaTime);

        // Z���� �������� ��ü ȸ��
        transform.Rotate(Vector3.forward, rotationAngle);
    }
}
