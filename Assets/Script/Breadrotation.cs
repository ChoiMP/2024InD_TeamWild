using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadrotation : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // 각도 변화 속도 (초당 1도)

    void Update()
    {
        // 0.01초 동안의 회전 각도 계산
        float rotationAngle = rotationSpeed * (0.01f / Time.deltaTime);

        // Z축을 기준으로 객체 회전
        transform.Rotate(Vector3.forward, rotationAngle);
    }
}
