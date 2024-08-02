using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    Transform _myTF;


    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ������Ʈ ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ ����
        }
        _myTF = GetComponent<Transform>();
        // ��ġ�� ���� ����
        _myTF.position = new Vector3(50.9f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_myTF.position.x > -50.3)
        {
            _myTF.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }
}
