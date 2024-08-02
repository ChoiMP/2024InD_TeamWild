using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    Transform _myTF;
    public float speedIncreaseFactor = 0.5f; // 속도를 얼마나 증가시킬지 결정하는 계수
    public float speedIncreaseInterval = 5f; // 속도 증가 주기 (초 단위)

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트 제거
        }

        _myTF = GetComponent<Transform>();
        // 위치를 직접 설정
        _myTF.position = new Vector3(50.9f, 0f, 0f);

        // 속도 증가 코루틴 시작
        StartCoroutine(IncreaseSpeedOverTime());
    }



    // Update is called once per frame
    void Update()
    {
        if (_myTF.position.x > -50.3)
        {
            _myTF.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            // 지정된 시간 동안 대기
            yield return new WaitForSeconds(speedIncreaseInterval);

            // 현재 속도의 0.5배만큼 속도 증가
            _speed += _speed * speedIncreaseFactor;
        }
    }
}
