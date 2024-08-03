using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GgumdolOnoff : MonoBehaviour
{
    public GameObject Ggumdol;
    private Animator ani;

    void Awake()
    {
        ani = GetComponent<Animator>();
        Ggumdol.SetActive(false);
    }

    private void Update()
    {
        if (BackGround_Mid.Instance._GOnoff)
        {
            Ggumdol.SetActive(true);
        }
        if (Ggumdol)
        {
            ani.Play("Ggombol");
        }
    }
}
