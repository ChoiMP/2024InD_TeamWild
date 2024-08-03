using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    public float _XPotion = 0.0f;
    private Transform _myTF;
    public float _speedIncreaseFactor = 0.5f; // 속도를 얼마나 증가시킬지 결정하는 계수
    public float _speedIncreaseInterval = 5f; // 속도 증가 주기 (초 단위)
    private float _speeddownTime = 1.0f; // 감속 시간 간격
    public float _xPostion = 0.0f;
    public float _xMove = 0.0f;

    [SerializeField]
    public int _speedFixed = 0;

    public float _gameTime = 55.0f;

    private void Start()
    {
        _myTF = GetComponent<Transform>();
        // 위치를 직접 설정
        _myTF.position = new Vector3(_XPotion, 0f, 0f);
    }

    private void Awake()
    {
        Instance = this;

        // 속도 증가 코루틴 시작
        StartCoroutine(IncreaseSpeedOverTime());
        // 60초 후에 속도를 고정시키는 코루틴 시작
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
            // 감속 코루틴이 이미 실행 중인지 확인
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
            // 지정된 시간 동안 대기
            yield return new WaitForSeconds(_speedIncreaseInterval);

            // 현재 속도의 _speedIncreaseFactor배만큼 속도 증가
            _speed += _speed * _speedIncreaseFactor;
        }
    }

    // 50초 후에 속도를 고정시키는 메서드
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

    private bool _isSlowingDown = false; // 감속 상태를 추적하는 변수

    private IEnumerator SlowDownSpeed()
    {
        _isSlowingDown = true; // 감속 시작
        while (_speed > 0)
        {
            _speed *= 0.8f; // 속도를 10%씩 감속
            yield return new WaitForSeconds(_speeddownTime); // 지정된 시간 대기
            if (_speed < 0.1f)
            {
                _speed = 0f; // 속도가 매우 작아지면 0으로 설정
                _speedFixed = 1; // 감속 완료 상태로 설정
                break;
            }
        }
        _isSlowingDown = false; // 감속 완료
    }
}
