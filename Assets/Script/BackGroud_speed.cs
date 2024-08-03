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

    public bool _speedFixed = false;

    public float _gameTime = 55.0f;

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
        // 60�� �Ŀ� �ӵ��� ������Ű�� �ڷ�ƾ ����
        StartCoroutine(FixSpeedAfter60Seconds());
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
        while (!_speedFixed)
        {
            // ������ �ð� ���� ���
            yield return new WaitForSeconds(_speedIncreaseInterval);

            // ���� �ӵ��� _speedIncreaseFactor�踸ŭ �ӵ� ����
            _speed += _speed * _speedIncreaseFactor;
        }
    }

    // 55�� �Ŀ� �ӵ��� ������Ű�� �޼���
    private IEnumerator FixSpeedAfter60Seconds()
    {
        yield return new WaitForSeconds(_gameTime);
        _speedFixed = true;
    }
}
