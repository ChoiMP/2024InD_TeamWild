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
