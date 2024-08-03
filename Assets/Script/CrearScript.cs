using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    // 애니메이션 트리거 메서드
    public void TriggerTextAnimation()
    {
        animator.SetTrigger("PlayTextAnimation");
    }
}
