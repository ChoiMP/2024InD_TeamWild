using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGamemanger : MonoBehaviour
{
    public static MapGamemanger Instance { get; private set; }
    public bool _break = true;
    private int _numrespawns=0;
    public Transform[] _respawns;
    public Transform[] _upObejct;
    public Transform[] _downObejct;

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
            case "Stage2":
            case "Stage3":
                fireCount = 2;
                Time = 1.5f;
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
        if (!_break)
        {
            Fire();
            _break = true;
        }
    }
}
