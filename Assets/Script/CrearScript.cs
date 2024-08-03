using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearScript : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    // �ִϸ��̼� Ʈ���� �޼���
    public void TriggerTextAnimation()
    {
        animator.SetTrigger("PlayTextAnimation");
    }
}
