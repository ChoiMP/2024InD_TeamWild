using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    public float _XPotion=0.0f;
    Transform _myTF;
    public float _speedIncreaseFactor = 0.5f; // 속도를 얼마나 증가시킬지 결정하는 계수
    public float _speedIncreaseInterval = 5f; // 속도 증가 주기 (초 단위)
    public float _xPostion = 0.0f;
    public float _xMove = 0.0f;

    public bool _speedFixed = false;

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
            // 지정된 시간 동안 대기
            yield return new WaitForSeconds(_speedIncreaseInterval);

            // 현재 속도의 _speedIncreaseFactor배만큼 속도 증가
            _speed += _speed * _speedIncreaseFactor;
        }
    }

    // 55초 후에 속도를 고정시키는 메서드
    private IEnumerator FixSpeedAfter60Seconds()
    {
        yield return new WaitForSeconds(_gameTime);
        _speedFixed = true;
    }
}
