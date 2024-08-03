using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGamemanger : MonoBehaviour
{
    public static MapGamemanger Instance { get; private set; }
    public bool _break = true;// ��ֹ��� �ν����°��� True, False
    private int _numrespawns = 0;//������ ��ġ ������
    public Transform[] _respawns;
    public Transform[] _upObejct;
    public Transform[] _downObejct;

    public bool _lastStage = false;


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

        // �� �̸��� ���� ȣ�� Ƚ�� ����
        switch (sceneName)
        {
            case "Stage1":
                fireCount = 1;
                Time = 2.0f;
                break;
            case "Stage2":
                fireCount = 2;
                Time = 1.5f;
                break;
            case "Stage3":
                fireCount = 2;
                Time = 1.0f;
                _lastStage = true;
                break;
            default:
                Debug.LogWarning("Unknown scene name");
                break;
        }

        // ȣ�� Ƚ���� ���������� �ڷ�ƾ ����
        if (fireCount > 0)
        {
            StartCoroutine(FireWithDelay(fireCount, Time));
        }

    }

    private IEnumerator FireWithDelay(int count, float time)
    {
        for (int i = 0; i < count; i++)
        {
            Fire(); // Fire �޼��� ȣ��
            yield return new WaitForSeconds(time); // 1.5�� ��ٸ���
        }
    }


    private void Update()
    {
        if (BackGround_Mid.Instance._breakStop)
        {
            if (BackGround_Mid.Instance._speedFixed == 0) // ��� �ӵ��� �������� �ʾ��� ��
            {
                if (!_break) // �ߴ� ���°� �ƴ� ��
                {
                    Fire();
                    _break = true;
                }
            }
        }
    }
}
