using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    public float _XPotion = 0.0f;
    private Transform _myTF;
    public float _speedIncreaseFactor = 0.5f; // �ӵ��� �󸶳� ������ų�� �����ϴ� ���
    public float _speedIncreaseInterval = 5f; // �ӵ� ���� �ֱ� (�� ����)
    private float _speeddownTime = 1.0f; // ���� �ð� ����
    public float _xPostion = 0.0f;
    public float _xMove = 0.0f;

    [SerializeField]
    public int _speedFixed = 0;

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

    void Update()
    {
        _myTF.Translate(Vector2.left * _speed * Time.deltaTime);
        if (_myTF.position.x < _xPostion)
        {
            _myTF.Translate(Vector2.right * _xMove);
        }
        if (_speedFixed == 2)
        {
            // ���� �ڷ�ƾ�� �̹� ���� ������ Ȯ��
            if (!_isSlowingDown)
            {
                StartCoroutine(SlowDownSpeed());
            }
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (_speedFixed == 0)
        {
            // ������ �ð� ���� ���
            yield return new WaitForSeconds(_speedIncreaseInterval);

            // ���� �ӵ��� _speedIncreaseFactor�踸ŭ �ӵ� ����
            _speed += _speed * _speedIncreaseFactor;
        }
    }

    // 50�� �Ŀ� �ӵ��� ������Ű�� �޼���
    private IEnumerator FixSpeedAfter60Seconds()
    {
        yield return new WaitForSeconds(_gameTime);
        MapGamemanger.Instance._break = true;
        if (MapGamemanger.Instance._lastStage)
        {
            _speedFixed = 2;
        }
        else
        {
            _speedFixed = 1;
        }
    }

    private bool _isSlowingDown = false; // ���� ���¸� �����ϴ� ����

    private IEnumerator SlowDownSpeed()
    {
        _isSlowingDown = true; // ���� ����
        while (_speed > 0)
        {
            _speed *= 0.8f; // �ӵ��� 10%�� ����
            yield return new WaitForSeconds(_speeddownTime); // ������ �ð� ���
            if (_speed < 0.1f)
            {
                _speed = 0f; // �ӵ��� �ſ� �۾����� 0���� ����
                _speedFixed = 1; // ���� �Ϸ� ���·� ����
                break;
            }
        }
        _isSlowingDown = false; // ���� �Ϸ�
    }
}
