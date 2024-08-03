using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGamemanger : MonoBehaviour
{
    public static MapGamemanger Instance { get; private set; }
    public int _breakcount;//장애물이 소환될 개수
    public bool _break = true;// 장애물이 부숴졌는가의 True, False
    private int _numrespawns = 0;//리스폰 위치 랜덤값
    public Transform[] _respawns;
    public Transform[] _upObejct;
    public Transform[] _downObejct;

    public Coroutine fireCoroutine; // Fire 코루틴 참조

    void Fire()
    {
        _numrespawns = UnityEngine.Random.Range(0, _respawns.Length);
        Transform respawn = _respawns[_numrespawns];

        if (_numrespawns == 0)
        {
            Instantiate(_upObejct[UnityEngine.Random.Range(0, _upObejct.Length)], respawn.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_downObejct[UnityEngine.Random.Range(0, _downObejct.Length)], respawn.position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        int fireCount = 0;
        float Time = 0.0f;

        // 씬 이름에 따라 호출 횟수 결정
        switch (sceneName)
        {
            case "Stage1":
                fireCount = 1;
                Time = 0.1f;
                break;
            case "Stage2":
                fireCount = 2;
                Time = 1.0f;
                break;
            case "Stage3":
                fireCount = 3;
                Time = 2.0f;
                break;
            default:
                Debug.LogWarning("Unknown scene name");
                break;
        }

        // 호출 횟수가 정해졌으면 코루틴 시작
        if (fireCount > 0)
        {
            StartCoroutine(FireWithDelay(fireCount, Time));
        }

    }

    private IEnumerator FireWithDelay(int count, float time)
    {
        for (int i = 0; i < count; i++)
        {
            Fire(); // Fire 메서드 호출
            yield return new WaitForSeconds(time); // 1.5초 기다리기
        }
    }

    private IEnumerator FireAtRandomIntervals()
    {
        while (true)
        {
            Fire();
            float waitTime = UnityEngine.Random.Range(0.5f, 4.0f);
            Debug.Log(waitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void Update()
    {
        if (!BackGroud_speed.Instance._speedFixed) // 배경 속도가 고정되지 않았을 때
        {
            if (!_break) // 중단 상태가 아닐 때
            {
                if (_breakcount > 0) // 남은 중단 횟수가 있을 때
                {
                    if (fireCoroutine == null)
                    {
                        _breakcount--;
                        fireCoroutine = StartCoroutine(FireAtRandomIntervals()); // 랜덤 간격으로 Fire 호출하는 코루틴 시작
                    }
                    _break = true; // 중단 상태로 변경
                }
            }
            else
            {
                if (fireCoroutine != null)
                {
                    StopCoroutine(fireCoroutine); // Fire 코루틴 중지
                    fireCoroutine = null;
                }
            }
        }
    }
}
