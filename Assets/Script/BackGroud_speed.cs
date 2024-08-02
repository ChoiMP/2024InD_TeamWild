using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroud_speed : MonoBehaviour
{
    public static BackGroud_speed Instance { get; private set; }
    public float _speed;
    Transform _myTF;
    public float speedIncreaseFactor = 0.5f; // �ӵ��� �󸶳� ������ų�� �����ϴ� ���
    public float speedIncreaseInterval = 5f; // �ӵ� ���� �ֱ� (�� ����)

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

        // �ӵ� ���� �ڷ�ƾ ����
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
            // ������ �ð� ���� ���
            yield return new WaitForSeconds(speedIncreaseInterval);

            // ���� �ӵ��� 0.5�踸ŭ �ӵ� ����
            _speed += _speed * speedIncreaseFactor;
        }
    }
}
