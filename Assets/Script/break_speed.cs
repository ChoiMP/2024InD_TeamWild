using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class break_speed : MonoBehaviour
{
    Transform _myTF;

    void Start()
    {
        _myTF = GetComponent<Transform>();
    }
    void Update()
    {
        _myTF.Translate(Vector2.left * BackGroud_speed.Instance._speed * Time.deltaTime);
        if (_myTF.position.x < -10)
        {
            MapGamemanger.Instance._break = false;
            Destroy(gameObject);
        }
    }
}
