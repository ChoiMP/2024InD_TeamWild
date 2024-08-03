using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    public float _XPotion=0.0f;
    Transform _myTF;
    public float _speedIncreaseFactor = 0.5f; // �ӵ��� �󸶳� ������ų�� �����ϴ� ���
    public float _speedIncreaseInterval = 5f; // �ӵ� ���� �ֱ� (�� ����)
    public float _xPostion = 0.0f;
    public float _xMove = 0.0f;

    private void Start()
    {
        _myTF = GetComponent<Transform>();
        // ��ġ�� ���� ����
        _myTF.position = new Vector3(_XPotion, 0f, 0f);
    }

    private void Awake()
    {
        Instance = this;

        // �ӵ� ���� �ڷ�ƾ ����
        StartCoroutine(IncreaseSpeedOverTime());
    }



    // Update is called once per frame
    void Update()
    {
        _myTF.Translate(Vector2.left * _speed * Time.deltaTime);
        if (_myTF.position.x < _xPostion)
        {
            _myTF.Translate(Vector2.right * _xMove);
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            // ������ �ð� ���� ���
            yield return new WaitForSeconds(_speedIncreaseInterval);

            // ���� �ӵ��� 0.5�踸ŭ �ӵ� ����
            _speed += _speed * _speedIncreaseFactor;
        }
    }
}
